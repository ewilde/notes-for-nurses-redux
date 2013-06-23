namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using MonoTouch.UIKit;

    using System.Drawing;

    public class PatientDetailViewController : UIViewController {
		UINavigationBar navBar;

		int speakerId;

	    PatientDetailView patientDetailView;

		int colWidth1 = 335;
		int colWidth2 = 433;
	
		public UIPopoverController Popover;

		public PatientDetailViewController (int speakerID) //, UIViewController PatientDetailView)
		{
			this.speakerId = speakerID;
			
			this.navBar = new UINavigationBar(new RectangleF(0,0,768, 44));
			this.navBar.SetItems(new UINavigationItem[]{new UINavigationItem("Patient & Session Info")},false);
			
			this.View.BackgroundColor = UIColor.LightGray;
			this.View.Frame = new RectangleF(0,0,768,768);

			this.patientDetailView = new PatientDetailView(-1);
			this.patientDetailView.Frame = new RectangleF(0,44,this.colWidth1,728);
			this.patientDetailView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

		
			this.View.AddSubview (this.patientDetailView);
			this.View.AddSubview (this.navBar);
		}

		public void Update(int speakerID) //, UIViewController view)
		{
			this.speakerId = speakerID;
			this.patientDetailView.Update (speakerID);
			this.patientDetailView.SetNeedsDisplay();
			

			if (this.Popover != null) {
				this.Popover.Dismiss (true);
			}
		}

	    public void AddNavBarButton (UIBarButtonItem button)
		{
			button.Title = "Speakers";
			this.navBar.TopItem.SetLeftBarButtonItem (button, false);
		}
		
		public void RemoveNavBarButton ()
		{
			this.navBar.TopItem.SetLeftBarButtonItem (null, false);
		}
	}
}