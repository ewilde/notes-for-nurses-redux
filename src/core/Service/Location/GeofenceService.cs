// -----------------------------------------------------------------------
// <copyright file="GeofenceService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;
    using System.Diagnostics;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class GeofenceService : IGeofenceService
    {
        private const int defaultTimeout = 5000;

        private Stopwatch timer;
        public ILocationListener Listener { get; set; }

        public IDistanceCalculatorService DistanceCalculatorService { get; set; }

        public ISessionContext SessionContext { get; private set; }

        public LocationCoordinate CentreOfGeofence
        {
            get
            {
                return this.SessionContext.GeofenceLocationCentre;
            }
        }

        public double RadiusOfGeofenceInMeters
        {
            get
            {
                return this.SessionContext.GeofenceRadiusSizeInMeters;
            }
        }

        public event EventHandler<EventArgs> OutsideFence;

        public event EventHandler<EventArgs> InsideFence;

        public GeofenceService(ILocationListener listener, IDistanceCalculatorService distanceCalculatorService, ISessionContext sessionContext)
        {
            this.Listener = listener;
            this.Listener.LocationChanged += this.OnLocationChanged;
            this.DistanceCalculatorService = distanceCalculatorService;
            this.SessionContext = sessionContext;
            this.timer = new Stopwatch();
        }

        public LocationCoordinate CurrentLocation { get; set; }

        public GeofenceState State { get; set; }

        public bool Timedout
        {
            get
            {
                return this.timer.ElapsedMilliseconds >= defaultTimeout;
            }
        }

        public bool Initialize()
        {
            this.State = GeofenceState.Initializing;
            this.StartTimer();
            try
            {
                Execute.Until(() => this.CurrentLocation != null || this.Timedout);
            }
            finally
            {
                this.StopTimer();
            }

            if (!this.Timedout)
            {
                this.FireEventsForLocationChanged();
            }
            else
            {
                this.State = GeofenceState.TimeoutWaitingForLocation;
            }

            return !this.Timedout && this.InsidePerimeter();
        }

        public bool InsidePerimeter()
        {
            return this.DistanceCalculatorService.DistanceBetween(this.CurrentLocation, this.CentreOfGeofence) <= this.RadiusOfGeofenceInMeters;
        }

        protected virtual void OnOutsideFence()
        {
            if (this.State == GeofenceState.OutsideFence)
            {
                return;
            }

            this.State = GeofenceState.OutsideFence;

            var handler = this.OutsideFence;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnInsideFence()
        {
            if (this.State == GeofenceState.InsideFence)
            {
                return;
            }

            this.State = GeofenceState.InsideFence;

            var handler = this.InsideFence;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void StartTimer()
        {
            this.timer.Reset();
            this.timer.Start();
        }

        private void StopTimer()
        {
            this.timer.Stop();
        }

        protected void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            this.CurrentLocation = e.Locations[0].Coordinate;
            this.FireEventsForLocationChanged();
        }

        private void FireEventsForLocationChanged()
        {
            if (!this.InsidePerimeter())
            {
                this.OnOutsideFence();
            }
            else
            {
                this.OnInsideFence();
            }
        }
    }
}