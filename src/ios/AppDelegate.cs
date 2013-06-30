using System;
using System.Globalization;
using System.IO;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace Edward.Wilde.Note.For.Nurses.iOS {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.DL;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.Common;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Network;

    [Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
        public IPatientFileUpdateManager PatientFileUpdateManager { get; set; }

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
            : this(TinyIoC.TinyIoCContainer.Current.Resolve<IPatientFileUpdateManager>())
        {            
        }

        public AppDelegate(IPatientFileUpdateManager patientFileUpdateManager)
        {
            this.PatientFileUpdateManager = patientFileUpdateManager;
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
            PatientDatabase.Initialize();				
			PatientFileUpdateManager.UpdateFinished += HandleFinishedUpdate;

			// start updating all data in the background
			// by calling this asynchronously, we must check to see if it's finished
			// everytime we want to use/display data.
			new Thread(new ThreadStart(() => {
				var prefs = NSUserDefaults.StandardUserDefaults;

                bool hasSeedData = PatientFileUpdateManager.DataExists;
				ConsoleD.WriteLine ("hasSeedData="+hasSeedData);
				if (!hasSeedData) {
					// only happens when the database is empty (or wasn't there); use local file update
					ConsoleD.WriteLine ("Load seed data");
					var appdir = NSBundle.MainBundle.ResourcePath;
					var seedDataFile = appdir + "/Images/SeedData.xml";
					string xml = System.IO.File.ReadAllText (seedDataFile);
					PatientFileUpdateManager.Update(xml);

					ConsoleD.WriteLine("Database lives at: "+PatientDatabase.DatabaseFilePath);
					// We SHOULDN'T skip backup because we are saving the Favorites in the same sqlite
					// database as the sessions are stored. A more iCloud-friendly design would be 
					// to keep the user-data separate from the server-generated data...
					NSFileManager.SetSkipBackupAttribute (PatientDatabase.DatabaseFilePath, true);
				} 
			})).Start();

			this.navigationView = new NavigationViewController ();
			
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
			PatientFileUpdateManager.UpdateFinished -= HandleFinishedUpdate;
		}
		
		/// <summary>
		/// When updates finished, save the time so we don't check again
		/// too soon.
		/// </summary>
		void HandleFinishedUpdate (object sender, EventArgs eventArgs)
		{
			var updateFinishedEventArgs = eventArgs as UpdateFinishedEventArgs;
			if (updateFinishedEventArgs != null) 
            {
                ConsoleD.WriteLine("Finished updating success {0} type {1}.", updateFinishedEventArgs.Success, updateFinishedEventArgs.UpdateType);

				if (updateFinishedEventArgs.Success)  
                {
                    if (updateFinishedEventArgs.UpdateType == UpdateType.SeedData)
                    {                        
                    }
                }				
			}
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