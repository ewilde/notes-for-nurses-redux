// -----------------------------------------------------------------------
// <copyright file="MapConfigurationView.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map
{
    using System;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
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

        public ISessionContext SessionContext { get; set; }

        public IScreenController ScreenController { get; set; }

        private MKMapView mapView;
        private SimpleAnnotation droppedPin;
        private UILongPressGestureRecognizer longPress;

        private UIToolbar toolbar;

        public MapConfigurationViewController(
            IGeofenceService geofenceService, 
            ISessionContext sessionContext,
            IScreenController screenController)
        {
            this.GeofenceService = geofenceService;
            this.SessionContext = sessionContext;
            this.ScreenController = screenController;
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
			if (currentLocation == null) 
			{
				currentLocation = this.SessionContext.GeofenceDefaultLocationCentre;
			}

            this.BuildToolbar();
            this.BuildMapView(currentLocation);
        }

        private void BuildMapView(LocationCoordinate currentLocation)
        {
            var visibleRegion = this.BuildVisibleRegion(currentLocation);
            this.mapView = new MKMapView()
            {
                ShowsUserLocation = true,
                ZoomEnabled = true
            };

            this.mapView.SizeToFit();
            this.mapView.Frame = new RectangleF(0, this.toolbar.Bounds.Height, this.View.Frame.Width, this.View.Frame.Height - this.toolbar.Bounds.Height);
            this.mapView.SetRegion(visibleRegion, true);
            this.mapView.Delegate = this.GeofenceMapDelegate;
            
            this.View.AddSubview(this.toolbar);
            this.View.AddSubview(this.mapView);
            this.SetupGestureInteraction();
        }

        private void BuildToolbar()
        {
            this.toolbar = new UIToolbar
                               {
                                   AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleWidth
                               };
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, this.TappedDone);

            this.toolbar.SetItems(new[] {
				doneButton
			}, true);
            this.toolbar.SizeToFit();
            this.toolbar.Frame = new RectangleF(0, 0, View.Bounds.Size.Width, toolbar.Bounds.Height);
        }

        public void TappedDone(object sender, EventArgs args)
        {
			if (this.GeofenceService.CurrentLocation == null) 
			{
				this.ScreenController.ShowMessage("Configuration", "Please allow this application to access your location.");
                this.GeofenceService.Initialize();
				return;                
			}

            if (this.GeofenceMapDelegate.Circle == null)
            {
                
				this.ScreenController.ShowMessage("Configuration", "Please choose a location, within which the application will operate.");
                return;                
            }

            this.UpdateConfiguration();
            this.Close();
        }

        private void Close()
        {
            this.ScreenController.MapConfigurationCompleted();
        }

        private void UpdateConfiguration()
        {
            this.SessionContext.GeofenceLocationCentre = 
                new LocationCoordinate(
                        this.GeofenceMapDelegate.Circle.Coordinate.Latitude,
                        this.GeofenceMapDelegate.Circle.Coordinate.Longitude);           
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

        /// <summary>
        /// Only allow iPad application to rotate, iPhone is always portrait
        /// </summary>
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            if (AppDelegate.IsPad)
                return true;
            else
                return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
        }
    }
}