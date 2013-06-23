//
// Inspired by the code written by Gregory S. Combs 
// hosted at https://github.com/grgcombs/IntelligentSplitViewController/
// under the Creative Commons Attribution 3.0 Unported License
//

namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI {
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    using MonoTouch.Foundation;
    using MonoTouch.ObjCRuntime;
    using MonoTouch.UIKit;

    public class IntelligentSplitViewController : UISplitViewController {

		NSObject ObserverWillRotate;
		NSObject ObserverDidRotate;

		public IntelligentSplitViewController ()
		{
			this.ObserverWillRotate = NSNotificationCenter.DefaultCenter.AddObserver(
					AppDelegate.NotificationWillChangeStatusBarOrientation, this.OnWillRotate);			
			this.ObserverDidRotate = NSNotificationCenter.DefaultCenter.AddObserver(
					AppDelegate.NotificationDidChangeStatusBarOrientation, this.OnDidRotate);			
		}
		protected override void Dispose (bool disposing)
		{
			NSNotificationCenter.DefaultCenter.RemoveObserver(this.ObserverWillRotate);
			NSNotificationCenter.DefaultCenter.RemoveObserver(this.ObserverDidRotate);
			base.Dispose (disposing);
		}
		protected void OnWillRotate (NSNotification notification)
		{
			if (!this.IsViewLoaded) return;
			if (notification == null) return;

			var o1 = notification.UserInfo.ValueForKey(new NSString("UIApplicationStatusBarOrientationUserInfoKey"));
			int o2 = Convert.ToInt32(o1.ToString ());
			UIInterfaceOrientation toOrientation =(UIInterfaceOrientation) o2;
			var notModal = !(this.TabBarController.ModalViewController == null);
			var isSelectedTab = (this.TabBarController.SelectedViewController == this);

			//ConsoleD.WriteLine ("toOrientation:"+toOrientation);
			//ConsoleD.WriteLine ("isSelectedTab:"+isSelectedTab);

			var duration = UIApplication.SharedApplication.StatusBarOrientationAnimationDuration;

			if (!isSelectedTab || !notModal) {
				base.WillRotate (toOrientation, duration);
				
				UIViewController master = this.ViewControllers[0];
				var theDelegate = this.Delegate;
				
				//YOU_DONT_FEEL_QUEAZY_ABOUT_THIS_BECAUSE_IT_PASSES_THE_APP_STORE
				UIBarButtonItem button = base.ValueForKey (new NSString("_barButtonItem")) as UIBarButtonItem;
				
				
				if (toOrientation == UIInterfaceOrientation.Portrait
				|| toOrientation == UIInterfaceOrientation.PortraitUpsideDown) {
					if (theDelegate != null && theDelegate.RespondsToSelector(new Selector("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:"))) {
						try {
							UIPopoverController popover = base.ValueForKey(new NSString("_hiddenPopoverController")) as UIPopoverController;
							theDelegate.WillHideViewController(this, master, button, popover);
						} catch (Exception e) {
							ConsoleD.WriteLine ("There was a nasty error while notifyng splitviewcontrollers of a portrait orientation change: " + e.Message);
						}
					}
		
				} else {
					if (theDelegate != null && theDelegate.RespondsToSelector(new Selector("splitViewController:willShowViewController:invalidatingBarButtonItem:"))) {
						try {
							theDelegate.WillShowViewController (this, master, button);
						} catch (Exception e) {
							ConsoleD.WriteLine ("There was a nasty error while notifyng splitviewcontrollers of a landscape orientation change: " + e.Message);
						}
					}
				}
	
			}
		}

		protected void OnDidRotate (NSNotification notification)
		{
			if (!this.IsViewLoaded) return;
			if (notification == null) return;

			var o1 = notification.UserInfo.ValueForKey(new NSString("UIApplicationStatusBarOrientationUserInfoKey"));
			int o2 = Convert.ToInt32(o1.ToString ());
			UIInterfaceOrientation toOrientation =(UIInterfaceOrientation) o2;
			var notModal = !(this.TabBarController.ModalViewController == null);
			var isSelectedTab = (this.TabBarController.SelectedViewController == this);
			if (!isSelectedTab || !notModal) {
				base.DidRotate(toOrientation);
			}
		}
	}
}

