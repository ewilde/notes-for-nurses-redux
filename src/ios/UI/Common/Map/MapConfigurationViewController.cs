// -----------------------------------------------------------------------
// <copyright file="MapConfigurationView.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map
{
    using System;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Gestures;

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
        private SimpleAnnotation droppedPin;
        private UILongPressGestureRecognizer longPress;

        public MapConfigurationViewController(IGeofenceService geofenceService)
        {
            this.GeofenceService = geofenceService;
            this.GeofenceMapDelegate = new GeofenceMapDelegate();
            if (!this.GeofenceService.IsInitialized)
            {
                this.GeofenceService.Initialize();
            }
        }

        protected GeofenceMapDelegate GeofenceMapDelegate
        {
            get;
            set;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var currentLocation = this.GeofenceService.CurrentLocation;
            var visibleRegion = BuildVisibleRegion(currentLocation);

            mapView = BuildMapView(true);
            mapView.SetRegion(visibleRegion, true);
            mapView.Delegate = this.GeofenceMapDelegate;

            this.View.AddSubview(mapView);
            this.SetupGestureInteraction();
        }

        private MKMapView BuildMapView(bool showUserLocation)
        {
            var view = new MKMapView()
            {
                ShowsUserLocation = showUserLocation,
                ZoomEnabled = true
            };

            view.SizeToFit();
            view.Frame = new RectangleF(0, 0, this.View.Frame.Width, this.View.Frame.Height);
            return view;
        }

        private MKCoordinateRegion BuildVisibleRegion(LocationCoordinate currentLocation)
        {
            var span = new MKCoordinateSpan(0.003125, 0.003125);
            var region = new MKCoordinateRegion(new CLLocationCoordinate2D(currentLocation.Latitude, currentLocation.Longitude), span);

            return region;
        }

        private void SetupGestureInteraction()
        {
            this.longPress = new UILongPressGestureRecognizer();
            this.longPress.AddTarget(this, new MonoTouch.ObjCRuntime.Selector("HandleLongPress"));
            this.longPress.Delegate = new GestureRecognizerDelegate();
            this.View.AddGestureRecognizer(longPress);
        }

        private void DropGeofenceCircle(PointF point)
        {
            CLLocationCoordinate2D convertedPoint = this.mapView.ConvertPoint(point, this.mapView);
            String pinTitle = String.Format("Centre of geofence.");
            String subCoordinates = String.Format("{0},{1}", convertedPoint.Latitude.ToString(), convertedPoint.Longitude.ToString());

            if (this.droppedPin != null)
            {
                this.mapView.RemoveAnnotation(this.droppedPin);
            }

            this.droppedPin = new SimpleAnnotation(convertedPoint, pinTitle, subCoordinates);
            this.mapView.AddAnnotation(droppedPin);
            
            
            if (this.GeofenceMapDelegate.Circle != null)
                this.mapView.RemoveOverlay(this.GeofenceMapDelegate.Circle);

            this.GeofenceMapDelegate.Circle = MKCircle.Circle(convertedPoint, this.GeofenceService.RadiusOfGeofenceInMeters);
            this.mapView.AddOverlay(this.GeofenceMapDelegate.Circle);		

            if (!this.mapView.VisibleMapRect.Contains(this.GeofenceMapDelegate.Circle.BoundingMap))
            {
                this.mapView.SetRegion(MKCoordinateRegion.FromMapRect(this.GeofenceMapDelegate.Circle.BoundingMap), true);
            }
        }

        [MonoTouch.Foundation.Export("HandleLongPress")]
        public void handleLongPress(UILongPressGestureRecognizer recognizer)
        {
            if (recognizer.State == UIGestureRecognizerState.Began)
            {
                PointF longPressPoint = recognizer.LocationInView(this.mapView);
                this.DropGeofenceCircle(longPressPoint);
            }
        }
    }
}