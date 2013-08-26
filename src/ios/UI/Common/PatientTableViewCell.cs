namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using System;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using MonoTouch.Dialog.Utilities;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    /// <remarks>
	/// Uses ImageLoader from MonoTouch.Dialog
	/// https://github.com/migueldeicaza/MonoTouch.Dialog/blob/master/MonoTouch.Dialog/Utilities/ImageLoader.cs
	/// </remarks>
	public class PatientTableViewCell : UITableViewCell, IImageUpdated {
		static UIFont bigFont = UIFont.FromName("Helvetica-Light", AppDelegate.Font16pt);
		static UIFont smallFont = UIFont.FromName("Helvetica-LightOblique", AppDelegate.Font10pt);
		UILabel nameLabel, companyLabel;
		UIImageView image;

		const int imageSpace = 44;
		const int padding = 8;
		
		public PatientTableViewCell (UITableViewCellStyle style, NSString ident, Patient showPatient) : base (style, ident)
		{
			this.SelectionStyle = UITableViewCellSelectionStyle.Blue;
		    showPatient.ItemUpdated += (sender, args) => this.UpdateCell(showPatient);

			this.nameLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = bigFont,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};
			this.companyLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = smallFont,
				TextColor = UIColor.DarkGray,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};

			this.image = new UIImageView();

			this.UpdateCell(showPatient);
			
			this.ContentView.Add (this.nameLabel);
			this.ContentView.Add (this.companyLabel);
			this.ContentView.Add (this.image);
		}
		
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var full = this.ContentView.Bounds;
			var bigFrame = full;
			
			bigFrame.X = imageSpace+padding+padding+5;
			bigFrame.Y = 13; // 15 -> 13
			bigFrame.Height = 23;
			bigFrame.Width -= (imageSpace+padding+padding);
			this.nameLabel.Frame = bigFrame;
			
			var smallFrame = full;
			smallFrame.X = imageSpace+padding+padding+5;
			smallFrame.Y = 15 + 23;
			smallFrame.Height = 15; // 12 -> 15
			smallFrame.Width -= (imageSpace+padding+padding);
			this.companyLabel.Frame = smallFrame;

			this.image.Frame = new RectangleF(8,8,44,44);
		}
		
		public void UpdateCell (Patient patient)
		{
			this.nameLabel.Text = patient.Name.ToString();
			this.companyLabel.Text = "COMPANY NAME?";

            this.image.Image = UIImage.FromBundle(patient.ProfilePicture);
		}

		public void UpdatedImage (Uri uri)
		{
			this.image.Image = ImageLoader.DefaultRequestImage(uri, this);
		}
	}	
}