namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls;

    using MonoTouch.UIKit;
    using MonoTouch.Dialog.Utilities;

    using System.Drawing;


    /// <summary>
	/// Used in:
	///  iPad   * SessionSpeakersMasterDetail
	///         * SpeakerSessionsMasterDetail
	///  NOT used on iPhone ~ see Common.iPhone.PatientListViewController which dups some of this
	/// </summary>
	public class PatientDetailView : UIView, IImageUpdated 
    {
        public IPatientManager PatientManager { get; set; }

        UILabel nameLabel, titleLabel, companyLabel;
		UITextView bioTextView;
		UIImageView image;
		
		int y = 0;
		int patientId;
		Patient showPatient;
		EmptyOverlay emptyOverlay;

		const int ImageSpace = 80;		
		
		public PatientDetailView(IPatientManager patientManager, int patientId)
		{
		    this.PatientManager = patientManager;
		    this.patientId = patientId;

			this.BackgroundColor = UIColor.White;
			
			this.nameLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font16pt),
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};
			this.titleLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.FromName ("Helvetica-LightOblique", AppDelegate.Font10pt),
				TextColor = UIColor.DarkGray,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};
			this.companyLabel = new UILabel () {
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10pt),
				TextColor = UIColor.DarkGray,
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f)
			};
			 this.bioTextView = new UITextView () {
				TextAlignment = UITextAlignment.Left,
				Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10_5pt),
				BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f),
				ScrollEnabled = true,
				Editable = false
			};
			this.image = new UIImageView();

			this.AddSubview (this.nameLabel);
			this.AddSubview (this.titleLabel);
			this.AddSubview (this.companyLabel);
			this.AddSubview (this.bioTextView);
			this.AddSubview (this.image);	
		}

		public override void LayoutSubviews ()
		{
			if (EmptyOverlay.ShowIfRequired (ref this.emptyOverlay, this.showPatient, this, "No Patient info", EmptyOverlayType.Speaker)) return;

			var full = this.Bounds;
			var bigFrame = full;
			
			bigFrame.X = ImageSpace+13+17;
			bigFrame.Y = this.y + 27; // 15 -> 13
			bigFrame.Height = 26;
			bigFrame.Width -= (ImageSpace+13+17);
			this.nameLabel.Frame = bigFrame;
			
			var smallFrame = full;
			smallFrame.X = ImageSpace+13+17;
			smallFrame.Y = this.y + 27+26;
			smallFrame.Height = 15; // 12 -> 15
			smallFrame.Width -= (ImageSpace+13+17);
			this.titleLabel.Frame = smallFrame;
			
			smallFrame.Y += this.y + 17;
			this.companyLabel.Frame = smallFrame;

			this.image.Frame = new RectangleF(13, this.y + 15, 80, 80);

			if (!String.IsNullOrEmpty(this.showPatient.Bio)) {
				if (AppDelegate.IsPhone) {
					// for now, hardcode iPhone dimensions to reduce regressions
					SizeF size = this.bioTextView.StringSize (this.showPatient.Bio
										, this.bioTextView.Font
										, new SizeF (310, 580)
										, UILineBreakMode.WordWrap);
					this.bioTextView.Frame = new RectangleF(5, this.y + 115, 310, size.Height);
				} else {
					var f = new SizeF (full.Width - 13 * 2, full.Height - (this.image.Frame.Y + 80 + 20));
//					SizeF size = bioTextView.StringSize (showPatient.Bio
//										, bioTextView.Font
//										, f
//										, UILineBreakMode.WordWrap);
					this.bioTextView.Frame = new RectangleF(5, this.image.Frame.Y + 80 + 10
										, f.Width
										, f.Height);
				}
			} else {
				this.bioTextView.Frame = new RectangleF(5, this.y + 115, 310, 30);
			}
		}
		
		// for masterdetail
		public void Update(int speakerID)
		{
			this.patientId = speakerID;
			this.showPatient = this.PatientManager.GetById(this.patientId);
			this.Update ();
			this.LayoutSubviews ();
		}

		public void Clear()
		{
			this.showPatient = null;
			this.nameLabel.Text = "";
			this.titleLabel.Text = "";
			this.companyLabel.Text = "";
			this.bioTextView.Text = "";
			this.image.Image = null;
			this.LayoutSubviews (); // show the grey 'no Patient' message
		}

		void Update()
		{
			if (this.showPatient == null) {this.nameLabel.Text ="not found"; return;}
			
			this.nameLabel.Text = this.showPatient.Name.ToString();
			this.titleLabel.Text = this.showPatient.Title;
			this.companyLabel.Text = this.showPatient.Company;

			if (!String.IsNullOrEmpty(this.showPatient.Bio)) {
				this.bioTextView.Text = this.showPatient.Bio;
				this.bioTextView.Font = UIFont.FromName ("Helvetica-Light", AppDelegate.Font10_5pt);
				this.bioTextView.TextColor = UIColor.Black;
			} else {
				this.bioTextView.Font = UIFont.FromName ("Helvetica-LightOblique", AppDelegate.Font10_5pt);
				this.bioTextView.TextColor = UIColor.Gray;
				this.bioTextView.Text = "No background information available.";
			}

            this.image.Image = ImageLoader.DefaultRequestImage(new Uri("https://en.gravatar.com/avatar/196d33ea9cdaf7817b98b981afe62c16?s=100"), this);			
		}

		public void UpdatedImage (Uri uri)
		{
			this.image.Image = ImageLoader.DefaultRequestImage(uri, this);
		}
	}
}