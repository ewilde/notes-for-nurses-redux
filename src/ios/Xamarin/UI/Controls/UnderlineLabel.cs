namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls {
    using MonoTouch.UIKit;
    using MonoTouch.CoreGraphics;
    using MonoTouch.Foundation;

    using global::System.Drawing;

    /// <summary>
	/// Custom label that appears to be a 'url link' (blue, underlined)
	/// </summary>
	public class UnderlineLabel : UILabel {
        public static readonly UIColor ColorTextLink = UIColor.FromRGB(9, 9, 238);		

		public UnderlineLabel ()
		{
		}
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			var st = new NSString(this.Text);
			var sz = st.StringSize (this.Font);

			CGContext context = UIGraphics.GetCurrentContext();
			context.SetFillColor(ColorTextLink.CGColor); 
			context.SetLineWidth(0.5f);
			context.MoveTo(0,sz.Height+2);
			context.AddLineToPoint(sz.Width,sz.Height+2);
			context.StrokePath();  
		}
	}
}

