// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="">
//   
// </copyright>
// <summary>
//   The app delegate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Edward.Wilde.Note.For.Nurses.iOS
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    using MonoTouch.Dialog.Utilities;
    using MonoTouch.Foundation;
    using MonoTouch.ObjCRuntime;
    using MonoTouch.UIKit;

    using TinyIoC;

    /// <summary>
    /// The app delegate.
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        #region Constants

        /// <summary>
        /// The font 10_5 pt.
        /// </summary>
        public const float Font10_5Pt = 14f;

        /// <summary>
        /// The font 10 pt.
        /// </summary>
        public const float Font10pt = 13f;

        /// <summary>
        /// The font 16 pt.
        /// </summary>
        public const float Font16pt = 22f;

        /// <summary>
        /// The font 7_5 pt.
        /// </summary>
        public const float Font7_5pt = 10f;

        /// <summary>
        /// The font 9 pt.
        /// </summary>
        public const float Font9pt = 12f;

        /// <summary>
        /// The image empty speaker.
        /// </summary>
        public const string ImageEmptySpeaker = "Images/Empty/speaker.png";

        #endregion

        #region Static Fields

        /// <summary>
        /// The color cell background home.
        /// </summary>
        public static readonly UIColor ColorCellBackgroundHome = UIColor.FromRGB(36, 54, 72);

        /// <summary>
        /// The color heading home.
        /// </summary>
        public static readonly UIColor ColorHeadingHome = UIColor.FromRGB(150, 210, 254);

        /// <summary>
        /// The color nav bar tint.
        /// </summary>
        public static readonly UIColor ColorNavBarTint = UIColor.FromRGB(55, 87, 118);

        /// <summary>
        /// The color text home.
        /// </summary>
        public static readonly UIColor ColorTextHome = UIColor.FromRGB(192, 205, 223);

        /// <summary>
        /// The notification did change status bar orientation.
        /// </summary>
        public static readonly NSString NotificationDidChangeStatusBarOrientation =
            new NSString("UIApplicationDidChangeStatusBarOrientationNotification");

        /// <summary>
        /// The notification favorite updated.
        /// </summary>
        public static readonly NSString NotificationFavoriteUpdated = new NSString("NotificationFavoriteUpdated");

        /// <summary>
        /// The notification orientation did change.
        /// </summary>
        public static readonly NSString NotificationOrientationDidChange =
            new NSString("UIDeviceOrientationDidChangeNotification");

        /// <summary>
        /// The notification will change status bar orientation.
        /// </summary>
        public static readonly NSString NotificationWillChangeStatusBarOrientation =
            new NSString("UIApplicationWillChangeStatusBarOrientationNotification");

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDelegate"/> class.
        /// </summary>
        public AppDelegate()
            : this(TinyIoCContainer.Current.Resolve<IObjectFactory>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDelegate"/> class.
        /// </summary>
        /// <param name="objectFactory">
        /// The object factory.
        /// </param>
        public AppDelegate(IObjectFactory objectFactory)
        {
            this.ObjectFactory = objectFactory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether has retina.
        /// </summary>
        public static bool HasRetina
        {
            get
            {
                if (UIScreen.MainScreen.RespondsToSelector(new Selector("scale")))
                {
                    return UIScreen.MainScreen.Scale == 2.0;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether is pad.
        /// </summary>
        public static bool IsPad
        {
            get
            {
                return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is phone.
        /// </summary>
        public static bool IsPhone
        {
            get
            {
                return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
            }
        }

        /// <summary>
        /// Gets or sets the object factory.
        /// </summary>
        public IObjectFactory ObjectFactory { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The finished launching.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            try
            {
                // couldn't do RespondsToSelector() on static 'Appearance' property)
                string majorVersionString = UIDevice.CurrentDevice.SystemVersion.Substring(0, 1);
                short majorVersion = Convert.ToInt16(majorVersionString);
                if (majorVersion >= 5)
                {
                    // gotta love Appearance in iOS5
                    // UINavigationBar.Appearance.TintColor = ColorNavBarTint;
                }

                this.ObjectFactory.Create<IStartupManager>().Run();
            }
            catch (Exception exception)
            {
                new UIAlertView("Unhandled error", exception.ToString(), null, "OK").Show();
            }

            return true;
        }

        /// <summary>
        /// When we receive a memory warning, clear the MT.D image cache
        /// </summary>
        /// <param name="application">
        /// The application.
        /// </param>
        public override void ReceiveMemoryWarning(UIApplication application)
        {
            ConsoleD.WriteLine("==== Received Memory Warning ====");
            ImageLoader.Purge();
        }

        /// <summary>
        /// The will terminate.
        /// </summary>
        /// <param name="application">
        /// The application.
        /// </param>
        public override void WillTerminate(UIApplication application)
        {
        }

        #endregion
    }
}