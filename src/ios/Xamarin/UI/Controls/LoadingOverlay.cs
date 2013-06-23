namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls {
    using MonoTouch.UIKit;

    using global::System.Drawing;

    /// <summary>
	/// A simple modal overlay that shows an activity spinner and loading message. To use, 
	/// instantiate with a RectangleF frame, and add to super view. When finished, call Hide().
	/// </summary>
	public class LoadingOverlay : UIView {
		// control declarations
		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;
		
		public LoadingOverlay (RectangleF frame) : base (frame)
		{
			// configurable bits
			this.BackgroundColor = UIColor.Black;
			this.Alpha = 0.75f;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			float labelHeight = 22;
			float labelWidth = this.Frame.Width - 20;
			
			// derive the center x and y
			float centerX = this.Frame.Width / 2;
			float centerY = this.Frame.Height / 2;
			
			// create the activity spinner, center it horizontall and put it 5 points above center x
			this.activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			this.activitySpinner.Frame = new RectangleF ( 
				centerX - (this.activitySpinner.Frame.Width / 2) ,
				centerY - this.activitySpinner.Frame.Height - 20 ,
				this.activitySpinner.Frame.Width ,
				this.activitySpinner.Frame.Height);
			this.activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			this.AddSubview (this.activitySpinner);
			this.activitySpinner.StartAnimating ();
			
			// create and configure the "Loading Data" label
			this.loadingLabel = new UILabel(new RectangleF (
				centerX - (labelWidth / 2),
				centerY + 20 ,
				labelWidth ,
				labelHeight
				));
			this.loadingLabel.BackgroundColor = UIColor.Clear;
			this.loadingLabel.TextColor = UIColor.White;
			this.loadingLabel.Text = "Loading Data...";
			this.loadingLabel.TextAlignment = UITextAlignment.Center;
			this.loadingLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			this.AddSubview (this.loadingLabel);
		}
		
		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate ( 
				0.5, // duration
				() => { this.Alpha = 0; }, 
				() => { this.RemoveFromSuperview(); }
			);
		}
	}
}