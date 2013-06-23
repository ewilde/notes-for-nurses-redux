namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Common {
    using System;
    using System.Diagnostics;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    /// <summary>
	/// Share a 'loading' screen between DialogViewControllers that
	/// are populated from network requests (Twitter and News)
	/// </summary>
	/// <remarks>
	/// This ViewController implements the data loading via a virtual
	/// method LoadData(), which must call StopLoadingScreen()
	/// </remarks>
	public class LoadingDialogViewController : DialogViewController {
		LoadingDialogView loadingDialogView;
		
		/// <summary>
		/// Set pushing=true so that the UINavCtrl 'back' button is enabled
		/// </summary>
		public LoadingDialogViewController (UITableViewStyle style, RootElement root) : base(style, root, true)
		{
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (this.Root == null || this.Root.Count == 0) {
				this.StartLoadingScreen("Loading...");
			
				NSTimer.CreateScheduledTimer (TimeSpan.FromMilliseconds (1), delegate {
					this.LoadData();
				});
			} else ConsoleD.WriteLine ("Dialog data already populated");
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return AppDelegate.IsPad;
		}

		/// <summary>
		/// Implement this in the subclass to actually load the data.
		/// You MUST call StopLoadingScreen() at the end of your implementation!
		/// </summary>
		protected virtual void LoadData() 
		{
		}
		
		/// <summary>
		/// Called automatically in ViewDidLoad()
		/// </summary>
		protected void StartLoadingScreen (string message)
		{
			using (var pool = new NSAutoreleasePool ()) {
				this.InvokeOnMainThread(delegate {
					
					var bounds = new RectangleF(0,0,768,1004);
					if (this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft
					|| this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight) {
						bounds = new RectangleF(0,0,1024,748);	
					} 

					if (AppDelegate.IsPhone)
						bounds = new RectangleF(0,0,320,460);

					this.loadingDialogView = new LoadingDialogView (message, bounds);
					// because DialogViewController is a UITableViewController,
					// we need to step OVER the UITableView, otherwise the loadingOverlay
					// sits *in* the scrolling area of the table
					this.View.Superview.Add (this.loadingDialogView);
					this.View.Superview.BringSubviewToFront (this.loadingDialogView);
					this.View.UserInteractionEnabled = false;
				});
			}
		}
		
		/// <summary>
		/// If a loading screen exists, it will fade it out.
		/// Your subclass MUST call this method once data has loaded (or a loading error occurred)
		/// to make the loading screen disappear and return control to the user
		/// </summary>
		protected void StopLoadingScreen ()
		{
			using (var pool = new NSAutoreleasePool ()) {
				this.InvokeOnMainThread(delegate {
					if (this.loadingDialogView != null) {
						Debug.WriteLine ("Fade out loading...");
						this.loadingDialogView.OnFinishedFadeOutAndRemove += delegate {
							if (this.loadingDialogView != null) {
								Debug.WriteLine ("Disposing of loadingDialogView object..");
								this.loadingDialogView.Dispose();
								this.loadingDialogView = null;
							}
						};
						this.loadingDialogView.FadeOutAndRemove ();
						this.View.UserInteractionEnabled = true;
					}
				});
			}
		}
	}
}