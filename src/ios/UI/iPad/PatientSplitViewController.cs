namespace Edward.Wilde.Note.For.Nurses.iOS.UI.iPad {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using MonoTouch.UIKit;

    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI;

    using TinyIoC;

    public class PatientSplitViewController : IntelligentSplitViewController {
        public IObjectFactory ObjectFactory { get; set; }

        PatientListViewController speakersList;
		PatientDetailViewController speakerDetailViewControllerWithSession;
		
		public PatientSplitViewController(IObjectFactory objectFactory)
		{
		    this.ObjectFactory = objectFactory;
		    this.Delegate = new SpeakerSplitViewDelegate();

            this.speakersList = this.ObjectFactory.Create<PatientListViewController>(new NamedParameterOverloads { { "patientSplitViewController", this } });
			this.speakerDetailViewControllerWithSession = this.ObjectFactory.Create<PatientDetailViewController>(new NamedParameterOverloads{{"patientId", -1}});
			
			this.ViewControllers = new UIViewController[]
				{this.speakersList, this.speakerDetailViewControllerWithSession};
		}

		public void ShowSpeaker (int speakerID)
		{
			this.speakerDetailViewControllerWithSession = this.ViewControllers[1] as PatientDetailViewController;
			this.speakerDetailViewControllerWithSession.Update(speakerID);
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
	}

 	public class SpeakerSplitViewDelegate : UISplitViewControllerDelegate
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