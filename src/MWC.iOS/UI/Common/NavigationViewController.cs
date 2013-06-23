using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace MWC.iOS.Screens.Common {
	public class NavigationViewController : UITabBarController {
		private UINavigationController speakerNav;
        private DialogViewController speakersScreen;
		private Screens.Common.About.AboutView aboutScreen;
        private UISplitViewController speakersSplitView;

	    public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// speakers tab
			if (AppDelegate.IsPhone) {
				speakersScreen = new Screens.iPhone.Speakers.PatientListViewController();			
				speakerNav = new UINavigationController();
				speakerNav.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
				speakerNav.PushViewController ( speakersScreen, false );
			} else {
				speakersSplitView = new MWC.iOS.Screens.iPad.Speakers.PatientSplitViewController();
				speakersSplitView.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
			}

			
			// about tab
			aboutScreen = new Screens.Common.About.AboutView();
			aboutScreen.TabBarItem = new UITabBarItem("About Xamarin"
										, UIImage.FromBundle("Images/Tabs/about.png"), 8);
			
			UIViewController[] viewControllers;
			// create our array of controllers
			if (AppDelegate.IsPhone) {
				viewControllers = new UIViewController[] {
					speakerNav,
					aboutScreen
				};
			} else {	// IsPad
				viewControllers = new UIViewController[] {
					speakersSplitView,
					aboutScreen
				};
			}
			
			// attach the view controllers
			ViewControllers = viewControllers;
			
			// tell the tab bar which controllers are allowed to customize. 
			// if we don't set  it assumes all controllers are customizable. 
			// if we set to empty array, NO controllers are customizable.
			CustomizableViewControllers = new UIViewController[] {};
			
			// set our selected item
		    if (AppDelegate.IsPhone)
		    {
                SelectedViewController = speakerNav;
		    }
		    else
		    {
		        SelectedViewController = speakersSplitView;
		    }
		}
		
		/// <summary>
		/// Only allow iPad application to rotate, iPhone is always portrait
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
			if (AppDelegate.IsPad)
	            return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
        }
	}
}