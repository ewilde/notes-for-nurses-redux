// -----------------------------------------------------------------------
// <copyright file="MapConfigurationView.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map
{
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.CoreLocation;
    using MonoTouch.MapKit;
    using MonoTouch.UIKit;

    /// <summary>
    /// The map configuration view controller.
    /// </summary>
    public class MapConfigurationViewController : UIViewController
    {
        public IGeofenceService GeofenceService { get; set; }

        private MKMapView mapView;

        public MapConfigurationViewController(IGeofenceService geofenceService)
        {
            this.GeofenceService = geofenceService;
            if (!this.GeofenceService.IsInitialized)
            {
                this.GeofenceService.Initialize();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var currentLocation = this.GeofenceService.CurrentLocation;
            var visibleRegion = BuildVisibleRegion(currentLocation);

            mapView = BuildMapView(true);
            mapView.SetRegion(visibleRegion, true);

            this.View.AddSubview(mapView);
        }

        private MKMapView BuildMapView(bool showUserLocation)
        {
            var view = new MKMapView()
            {
                ShowsUserLocation = showUserLocation
            };

            view.SizeToFit();
            view.Frame = new RectangleF(0, 0, this.View.Frame.Width, this.View.Frame.Height);
            return view;
        }

        private MKCoordinateRegion BuildVisibleRegion(LocationCoordinate currentLocation)
        {
            var span = new MKCoordinateSpan(0.2, 0.2);
            var region = new MKCoordinateRegion(new CLLocationCoordinate2D(currentLocation.Latitude, currentLocation.Longitude), span);

            return region;
        }
    }
}