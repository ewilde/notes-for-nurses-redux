namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using MonoTouch.UIKit;

    using System.Drawing;

    using TinyIoC;

    public class PatientDetailViewController : UIViewController {
        public IObjectFactory ObjectFactory { get; set; }

        UINavigationBar navBar;

		int patientId;

	    PatientDetailView patientDetailView;

		int colWidth1 = 335;
		int colWidth2 = 433;
	
		public UIPopoverController Popover;

		public PatientDetailViewController(IObjectFactory objectFactory, int patientId)
		{
		    this.ObjectFactory = objectFactory;
		    this.patientId = patientId;
			
			this.navBar = new UINavigationBar(new RectangleF(0,0,768, 44));
			this.navBar.SetItems(new[]{new UINavigationItem("Patient & Session Info")},false);
			
			this.View.BackgroundColor = UIColor.LightGray;
			this.View.Frame = new RectangleF(0,0,768,768);

            this.patientDetailView = this.ObjectFactory.Create<PatientDetailView>(new NamedParameterOverloads { { "patientId", -1 } });
			this.patientDetailView.Frame = new RectangleF(0,44,this.colWidth1,728);
			this.patientDetailView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

		
			this.View.AddSubview (this.patientDetailView);
			this.View.AddSubview (this.navBar);
		}

		public void Update(int patientId)
		{
			this.patientId = patientId;
			this.patientDetailView.Update (patientId);
			this.patientDetailView.SetNeedsDisplay();
			

			if (this.Popover != null) {
				this.Popover.Dismiss (true);
			}
		}

	    public void AddNavBarButton (UIBarButtonItem button)
		{
			button.Title = "Patients";
			this.navBar.TopItem.SetLeftBarButtonItem (button, false);
		}
		
		public void RemoveNavBarButton ()
		{
			this.navBar.TopItem.SetLeftBarButtonItem (null, false);
		}
	}
}