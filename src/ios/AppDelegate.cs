using System;
using System.Globalization;
using System.IO;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace Edward.Wilde.Note.For.Nurses.iOS {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;
    using Edward.Wilde.Note.For.Nurses.iOS.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Network;

    [Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
        public IObjectFactory ObjectFactory { get; set; }

        public const string ImageEmptySpeaker  = "Images/Empty/speaker.png";
		
		public const float Font16pt = 22f;
		public const float Font10_5pt = 14f;
		public const float Font10pt = 13f;
		public const float Font9pt = 12f;
		public const float Font7_5pt = 10f;

		public static readonly UIColor ColorNavBarTint = UIColor.FromRGB (55, 87 ,118);
		public static readonly UIColor ColorTextHome = UIColor.FromRGB (192, 205, 223);
		public static readonly UIColor ColorHeadingHome = UIColor.FromRGB (150, 210, 254);
		public static readonly UIColor ColorCellBackgroundHome = UIColor.FromRGB (36, 54, 72);
		
		
		public static readonly NSString NotificationWillChangeStatusBarOrientation = new NSString("UIApplicationWillChangeStatusBarOrientationNotification");
		public static readonly NSString NotificationDidChangeStatusBarOrientation = new NSString("UIApplicationDidChangeStatusBarOrientationNotification");		
		public static readonly NSString NotificationOrientationDidChange = new NSString("UIDeviceOrientationDidChangeNotification");
		public static readonly NSString NotificationFavoriteUpdated = new NSString("NotificationFavoriteUpdated");
		// class-level declarations
		UIWindow window;
		NavigationViewController navigationView;


        public AppDelegate()
            : this(TinyIoC.TinyIoCContainer.Current.Resolve<IObjectFactory>()
            )
        {            
        }

        public AppDelegate(IObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
        }

        public static bool IsPhone {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
			}
		}
		public static bool IsPad {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
			}
		}
		public static bool HasRetina {
			get {
				if (MonoTouch.UIKit.UIScreen.MainScreen.RespondsToSelector(new Selector("scale")))
					return (MonoTouch.UIKit.UIScreen.MainScreen.Scale == 2.0);
				else
					return false;
			}
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            this.ObjectFactory.Create<IStartupManager>().Run();

			this.navigationView = this.ObjectFactory.Create<NavigationViewController>();
			
			// couldn't do RespondsToSelector() on static 'Appearance' property)
			var majorVersionString = UIDevice.CurrentDevice.SystemVersion.Substring (0,1);
			var majorVersion = Convert.ToInt16(majorVersionString);
			if (majorVersion >= 5) { // gotta love Appearance in iOS5
				UINavigationBar.Appearance.TintColor = ColorNavBarTint;			
			}
			window.RootViewController = this.navigationView;
			window.MakeKeyAndVisible ();

			return true;
		}
		
		public override void WillTerminate (UIApplication application)
		{
		}
		
		
		/// <summary>
		/// When we receive a memory warning, clear the MT.D image cache
		/// </summary>
		public override void ReceiveMemoryWarning (UIApplication application)
		{
			ConsoleD.WriteLine("==== Received Memory Warning ====");
			MonoTouch.Dialog.Utilities.ImageLoader.Purge();
		}
	}
}