// -----------------------------------------------------------------------
// <copyright file="GeofenceService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class GeofenceService : IGeofenceService
    {
        public ILocationListener Listener { get; set; }

        public IDistanceCalculatorService DistanceCalculatorService { get; set; }

        public GeofenceService(ILocationListener listener, IDistanceCalculatorService distanceCalculatorService)
        {
            this.Listener = listener;
            this.Listener.LocationChanged += this.OnLocationChanged;
            this.DistanceCalculatorService = distanceCalculatorService;
        }

        public bool Initialize()
        {
            return false;
        }

        public bool InsidePerimeter(LocationCoordinate currentLocation, LocationCoordinate centreOfPerimeter)
        {
            return false;
        }

        protected void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            this.DistanceCalculatorService.DistanceBetween(new LocationCoordinate(), e.Locations[0].Coordinate);
        }
    }
}