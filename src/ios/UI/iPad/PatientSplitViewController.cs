namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using MonoTouch.UIKit;

    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI;

    using TinyIoC;

    public class PatientSplitViewController : IntelligentSplitViewController {
        public IObjectFactory ObjectFactory { get; set; }

        PatientListViewController patientListController;
		PatientDetailViewController patientDetailController;
		
		public PatientSplitViewController(IObjectFactory objectFactory)
		{
		    this.ObjectFactory = objectFactory;
		    this.Delegate = new PatientSplitViewDelegate();

            this.patientListController = this.ObjectFactory.Create<PatientListViewController>(new NamedParameterOverloads { { "patientSplitViewController", this } });
			this.patientDetailController = this.ObjectFactory.Create<PatientDetailViewController>();
			
			this.ViewControllers = new UIViewController[]
				{this.patientListController, this.patientDetailController};
		}

		public void ShowPatient (Patient patient)
		{
			this.patientDetailController = this.ViewControllers[1] as PatientDetailViewController;
			this.patientDetailController.Update(patient);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
	}

 	public class PatientSplitViewDelegate : UISplitViewControllerDelegate
    {
		public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
		{
			return inOrientation == UIInterfaceOrientation.Portrait
				|| inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
		}

		public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
		{
			PatientDetailViewController dvc = svc.ViewControllers[1] as PatientDetailViewController;
			
			if (dvc != null) {
				dvc.AddNavBarButton (barButtonItem);
				dvc.Popover = pc;
			}
		}
		
		public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
		{
			PatientDetailViewController dvc = svc.ViewControllers[1] as PatientDetailViewController;
			
			if (dvc != null) {
				dvc.RemoveNavBarButton ();
				dvc.Popover = null;
			}
		}
	}
}