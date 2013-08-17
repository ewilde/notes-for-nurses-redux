﻿// -----------------------------------------------------------------------
// <copyright file="ScreenController.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common.Map;

    using MonoTouch.UIKit;

    public class ScreenController : IScreenController
    {
        public IObjectFactory ObjectFactory { get; set; }

        private UIWindow window;
        
        public ScreenController(IObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
        }

        protected bool Initialized { get; set; }

        protected void Initialize()
        {
            if (this.Initialized)
            {
                return;
            }

            // create a new window instance based on the screen size
            this.window = new UIWindow(UIScreen.MainScreen.Bounds);
            this.Initialized = true;
        }

        public void StartConfiguration()
        {
            this.Initialize();
            this.window.RootViewController = this.ObjectFactory.Create<MapConfigurationViewController>();
            window.MakeKeyAndVisible();
        }

        public void MapConfigurationCompleted()
        {
            this.ShowMessage("Configuration", "Now configure password.");
            //this.window.RootViewController = this.ObjectFactory.Create<PasswordConfigurationViewController>(SettingKey.ReadPassword);
        }

        public void ShowHomeScreen()
        {
            this.Initialize();
            this.window.RootViewController = this.ObjectFactory.Create<NavigationViewController>();
            window.MakeKeyAndVisible();
        }

        public void ShowExitScreen(string message)
        {
            this.Initialize();
            new UIAlertView("Exiting application", message, null, "OK", null).Show();
        }

        public void ShowMessage(string title, string message)
        {
            new UIAlertView(title, message, null, "OK", null).Show();
        }
    }
}