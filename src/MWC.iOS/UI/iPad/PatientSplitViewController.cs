using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MWC.iOS.Screens.iPhone.Speakers;

namespace MWC.iOS.Screens.iPad.Speakers {
    using MWC.iOS.Xamarin.UI;

    public class PatientSplitViewController : IntelligentSplitViewController {
		PatientListViewController speakersList;
		PatientDetailViewController speakerDetailViewControllerWithSession;
		
		public PatientSplitViewController ()
		{
			Delegate = new SpeakerSplitViewDelegate();
			
			speakersList = new PatientListViewController(this);
			this.speakerDetailViewControllerWithSession = new PatientDetailViewController(-1);
			
			ViewControllers = new UIViewController[]
				{speakersList, this.speakerDetailViewControllerWithSession};
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