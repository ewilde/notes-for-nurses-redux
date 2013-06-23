namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls {
    using MonoTouch.UIKit;

    using global::System.Drawing;

    public class EmptyOverlay : UIView {
		UILabel emptyLabel;
		UIImageView emptyImageView;

		public EmptyOverlay (RectangleF frame, string caption, EmptyOverlayType type) : base (frame)
		{
			// configurable bits
			this.BackgroundColor = UIColor.LightGray;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			var imageFilename = "";
			switch (type) {
			case EmptyOverlayType.Speaker:   
                imageFilename = AppDelegate.ImageEmptySpeaker; break;
            default: imageFilename = AppDelegate.ImageEmptySpeaker; break;
			}
			var img = UIImage.FromFile (imageFilename);

			float labelHeight = 22;
			float labelWidth = this.Frame.Width - 20;
			
			// derive the center x and y
			float centerX = this.Frame.Width / 2;
			float centerY = this.Frame.Height / 2;
			
			this.emptyImageView = new UIImageView (new RectangleF(
				centerX - (img.Size.Width / 2),
				centerY - img.Size.Height - 25,
				img.Size.Width ,
				img.Size.Height
			));
			this.emptyImageView.Image = img;
			this.emptyImageView.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;


			// create and configure the "Loading Data" label
			this.emptyLabel = new UILabel(new RectangleF (
				centerX - (labelWidth / 2),
				centerY + 25,
				labelWidth ,
				labelHeight
				));
			this.emptyLabel.BackgroundColor = UIColor.Clear;
			this.emptyLabel.TextColor = UIColor.FromRGB(136, 136, 136); //UIColor.White;
			this.emptyLabel.Font = UIFont.FromName ("Helvetica-Light",AppDelegate.Font16pt);
			this.emptyLabel.Text = caption;
			this.emptyLabel.TextAlignment = UITextAlignment.Center;
			this.emptyLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			
			this.AddSubview (this.emptyImageView);
			this.AddSubview (this.emptyLabel);
		}
		/// <summary>
		/// Static helper to show the 'empty overlay' if a business object is null
		/// </summary>
		/// <returns>
		/// True if it was required, false if not (ie. the business object is NOT NULL)
		/// </returns>
		public static bool ShowIfRequired (ref EmptyOverlay emptyOverlay
						, object toShow
						, UIView view
						, string caption
						, EmptyOverlayType type) {
			if (toShow == null) {
				if (emptyOverlay == null) {
					emptyOverlay = new EmptyOverlay(view.Bounds, caption, type);
					view.AddSubview (emptyOverlay);
				}
				return true;
			} else{
				if (emptyOverlay != null) {
					emptyOverlay.RemoveFromSuperview ();
					emptyOverlay = null;
				}
			}
			return false;
		}
	}
}