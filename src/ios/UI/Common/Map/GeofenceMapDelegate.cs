// -----------------------------------------------------------------------
// <copyright file="GeofenceMapDelegate.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map
{
    using MonoTouch.MapKit;
    using MonoTouch.UIKit;

    public class GeofenceMapDelegate : MKMapViewDelegate
    {
        private MKCircleView circleView;
        private MKCircle circle;

        public MKCircleView CircleView
        {
            get
            {
                return this.circleView;
            }
        }

        public MKCircle Circle
        {
            get
            {
                return this.circle;
            }
            set
            {
                this.circle = value;
            }
        }

        public override MKOverlayView GetViewForOverlay(MKMapView mapView, MonoTouch.Foundation.NSObject overlay)
        {
            if (this.CircleView == null)
            {
                this.circleView = new MKCircleView(this.circle)
                                      {                                          
                                          StrokeColor = UIColor.Red, LineWidth = 2.0f
                                      };
            }

            return this.CircleView;
        }
    }
}