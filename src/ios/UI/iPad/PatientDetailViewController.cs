namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using MonoTouch.UIKit;

    using System.Drawing;

    using TinyIoC;

    public class PatientDetailViewController : UIViewController {
        public IObjectFactory ObjectFactory { get; set; }

        UINavigationBar navBar;
	    PatientDetailView patientDetailView;

		int colWidth1 = 335;
		int colWidth2 = 433;
	
		public UIPopoverController Popover;

        private Patient currentPatient;

        public IPatientManager PatientManager { get; set; }

        public PatientDetailViewController(IObjectFactory objectFactory)
		{
		    this.ObjectFactory = objectFactory;
		    this.PatientManager = this.ObjectFactory.Create<IPatientManager>();
			
			this.navBar = new UINavigationBar(new RectangleF(0,0,768, 44));
            this.navBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			this.navBar.SetItems(
                new[]
                {
                    new UINavigationItem("Patient & Session Info")
                },false);
			
            this.navBar.TopItem.SetRightBarButtonItem(this.EditButtonItem, false);
			this.View.BackgroundColor = UIColor.LightGray;
			this.View.Frame = new RectangleF(0,0,768,768);

            this.patientDetailView = this.ObjectFactory.Create<PatientDetailView>();
			this.patientDetailView.Frame = new RectangleF(0,44,this.colWidth1 + this.colWidth2, 728);
			this.patientDetailView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

		
			this.View.AddSubview (this.patientDetailView);
			this.View.AddSubview (this.navBar);
		}

        public override void SetEditing(bool editing, bool animated)
        {
            base.SetEditing(editing, animated);

            if (editing)
            {
                this.patientDetailView.StartEditing();
            }
            else
            {
                this.patientDetailView.FinishedEditing();
                this.Save();
            }
        }

        private void Save()
        {
            this.PatientManager.Save(this.currentPatient);
        }

        public void Update(Patient patient)
		{
			this.currentPatient = patient;
            this.patientDetailView.Update(this.currentPatient);
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