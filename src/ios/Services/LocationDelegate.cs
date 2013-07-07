namespace Edward.Wilde.Note.For.Nurses.iOS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.CoreLocation;

    public class LocationDelegate : CLLocationManagerDelegate
    {
        private readonly LocationService locationService;

        public LocationDelegate(LocationService locationService)
        {
            this.locationService = locationService;
        }

        // called for iOS5.x and earlier
        [Obsolete("Deprecated in iOS 6.0", false)]
        public override void UpdatedLocation(CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation)
        {
            this.locationService.OnLocationChanged(new[] { newLocation });
        }

        // called for iOS6 and later

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            this.locationService.OnLocationChanged(locations);
        }

       

        public override void UpdatedHeading(CLLocationManager manager, CLHeading newHeading)
        {
            throw new NotImplementedException();
        }
    }
}