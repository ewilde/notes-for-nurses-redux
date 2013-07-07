// -----------------------------------------------------------------------
// <copyright file="LocationService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.CoreLocation;

    public class LocationService : ILocationService
    {
        private CLLocationManager locationManager;
        
        private readonly IObjectFactory objectFactory;

        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        public LocationService(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public void StartListening(LocationSettings settings)
        {
            if (this.locationManager != null)
            {
                return;
            }

            this.locationManager = new CLLocationManager
                {
                    DesiredAccuracy = this.GetAccuracyFromSetting(settings.DesiredAccuracy),
                    ActivityType = CLActivityType.Other,
                    Delegate = new LocationDelegate(this)
                };

            this.locationManager.StartMonitoringSignificantLocationChanges();
        }               

        public void StopListening()
        {
            this.locationManager.StopMonitoringSignificantLocationChanges();
        }

        public void OnLocationChanged(IEnumerable<CLLocation> locationsUpdated)
        {
            if (this.LocationChanged != null)
            {
                this.LocationChanged(
                    this,
                    new LocationChangedEventArgs(
                        locationsUpdated.Select(
                            x =>
                            {
                                var location = objectFactory.Create<Location>();
                                location.Coordinate = new LocationCoordinate(x.Coordinate.Longitude, x.Coordinate.Latitude);
                                location.Direction = Convert.ToInt32(x.Course);
                                return location;
                            })));
            }
        }

        private double GetAccuracyFromSetting(LocationAccuracy locationAccuracy)
        {
            switch (locationAccuracy)
            {
                case LocationAccuracy.AccuracyBest:
                    return CLLocation.AccuracyBest;
                case LocationAccuracy.AccuracyHundredMeters:
                    return CLLocation.AccuracyBest;
                case LocationAccuracy.AccuracyKilometer:
                    return CLLocation.AccuracyBest;
                case LocationAccuracy.AccuracyNearestTenMeters:
                    return CLLocation.AccuracyBest;
                case LocationAccuracy.AccuracyThreeKilometers:
                    return CLLocation.AccuracyBest;
                case LocationAccuracy.AccurracyBestForNavigation:
                    return CLLocation.AccuracyBest;
                default:
                    throw new ArgumentOutOfRangeException("locationAccuracy", locationAccuracy , "Unknown accuracy setting");
            }
        }
    }
}