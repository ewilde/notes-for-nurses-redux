// -----------------------------------------------------------------------
// <copyright file="ScreenController.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;

    using MonoTouch.UIKit;

    public class ScreenController : IScreenController
    {
        public IObjectFactory ObjectFactory { get; set; }

        private UIWindow window;
        
        public ScreenController(IObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
        }

        protected void Initialize()
        {
            // create a new window instance based on the screen size
            this.window = new UIWindow(UIScreen.MainScreen.Bounds);
            
        }
        public void ShowConfigurationScreen()
        {
            new UIAlertView("Configuration", "Must configure the application", null, "OK", null).Show();
        }

        public void ShowHomeScreen()
        {
            this.window.RootViewController = this.ObjectFactory.Create<NavigationViewController>();
            window.MakeKeyAndVisible();
        }

        public void ShowExitScreen(string message)
        {
            new UIAlertView("Exiting application", message, null, "OK", null).Show();
        }
    }
}