namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;

    using MonoTouch.Dialog;
    using MonoTouch.UIKit;

    public class NavigationViewController : UITabBarController {
		private UINavigationController speakerNav;
        private DialogViewController speakersScreen;
		private AboutView aboutScreen;
        private UISplitViewController speakersSplitView;

	    public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// speakers tab
			if (AppDelegate.IsPhone) {
				this.speakersScreen = new PatientListViewController();			
				this.speakerNav = new UINavigationController();
				this.speakerNav.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
				this.speakerNav.PushViewController ( this.speakersScreen, false );
			} else {
				this.speakersSplitView = new PatientSplitViewController();
				this.speakersSplitView.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
			}

			
			// about tab
			this.aboutScreen = new AboutView();
			this.aboutScreen.TabBarItem = new UITabBarItem("About Xamarin"
										, UIImage.FromBundle("Images/Tabs/about.png"), 8);
			
			UIViewController[] viewControllers;
			// create our array of controllers
			if (AppDelegate.IsPhone) {
				viewControllers = new UIViewController[] {
					this.speakerNav,
					this.aboutScreen
				};
			} else {	// IsPad
				viewControllers = new UIViewController[] {
					this.speakersSplitView,
					this.aboutScreen
				};
			}
			
			// attach the view controllers
			this.ViewControllers = viewControllers;
			
			// tell the tab bar which controllers are allowed to customize. 
			// if we don't set  it assumes all controllers are customizable. 
			// if we set to empty array, NO controllers are customizable.
			this.CustomizableViewControllers = new UIViewController[] {};
			
			// set our selected item
		    if (AppDelegate.IsPhone)
		    {
                this.SelectedViewController = this.speakerNav;
		    }
		    else
		    {
		        this.SelectedViewController = this.speakersSplitView;
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