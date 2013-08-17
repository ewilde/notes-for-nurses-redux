// -----------------------------------------------------------------------
// <copyright file="SimpleAnnotation.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map
{
    using MonoTouch.CoreLocation;
    using MonoTouch.MapKit;

    public class SimpleAnnotation : MKAnnotation
    {

        string title;
        string subTitle;

        public SimpleAnnotation(CLLocationCoordinate2D coordinate, string title, string subTitle)
        {
            this.Coordinate = coordinate;
            this.title = title;
            this.subTitle = subTitle;
        }

        public override string Title
        {
            get
            {
                return title;
            }
        }

        public override string Subtitle
        {
            get
            {
                return this.subTitle;
            }
        }

        public override CLLocationCoordinate2D Coordinate { get; set; }
    }

}