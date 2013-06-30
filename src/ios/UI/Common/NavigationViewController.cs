namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;

    using MonoTouch.Dialog;
    using MonoTouch.UIKit;

    using TinyIoC;

    public class NavigationViewController : UITabBarController {
        private UINavigationController speakerNav;

        private DialogViewController speakersScreen;

        private AboutView aboutScreen;

        private UISplitViewController speakersSplitView;

        private readonly bool propertiesAssigned;

        public IObjectFactory ObjectFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewController"/> class.
        /// Note that because the default constructor for <see cref="UITabBarController"/> calls the <see cref="ViewDidLoad"/>
        /// method we have added a check to call it afterwards instead once properties have been initialized.
        /// </summary>
        /// <param name="objectFactory">The view factory.</param>
        public NavigationViewController(IObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
            this.propertiesAssigned = true;
            this.ViewDidLoad();
        }

        public override void ViewDidLoad ()
		{
            if (!this.propertiesAssigned)
            {
                return;
            }

			base.ViewDidLoad ();
			
			// speakers tab
			if (AppDelegate.IsPhone) {
                this.speakersScreen = this.ObjectFactory.Create<PatientListViewController>(new NamedParameterOverloads { { "patientSplitViewController", null } });
				this.speakerNav = this.ObjectFactory.Create<UINavigationController>();
				this.speakerNav.TabBarItem = new UITabBarItem("Patients"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
				this.speakerNav.PushViewController ( this.speakersScreen, false );
			} else {
                this.speakersSplitView = this.ObjectFactory.Create<PatientSplitViewController>();
				this.speakersSplitView.TabBarItem = new UITabBarItem("Patients"
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