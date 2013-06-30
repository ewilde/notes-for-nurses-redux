namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Common {
    using System;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.UI.Controls;

    using MonoTouch.Dialog;
    using MonoTouch.UIKit;

    /// <summary>
	/// Base class for loading screens: Home, Speakers, Sessions
	/// </summary>
	/// <remarks>
	/// This ViewController implements the data loading via a virtual
	/// method LoadData(), which must call StopLoadingScreen()
	/// </remarks>
	public partial class UpdateManagerLoadingDialogViewController : DialogViewController {
        public IPatientFileUpdateManager PatientFileUpdateManager { get; set; }

        LoadingOverlay loadingOverlay;

		/// <summary>
		/// Set pushing=true so that the UINavCtrl 'back' button is enabled
		/// </summary>
		public UpdateManagerLoadingDialogViewController (IPatientFileUpdateManager patientFileUpdateManager) : base (UITableViewStyle.Plain, null, true)
		{
		    PatientFileUpdateManager = patientFileUpdateManager;
		}

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.PatientFileUpdateManager.UpdateFinished += this.HandleUpdateFinished;
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if(this.PatientFileUpdateManager.UpdateInProgress) {
				if (this.loadingOverlay == null) {
					var bounds = new RectangleF(0,0,768,1004);
					if (this.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft
					|| this.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight) {
						bounds = new RectangleF(0,0,1024,748);	
					} 

					this.loadingOverlay = new LoadingOverlay (bounds);
					// because DialogViewController is a UITableViewController,
					// we need to step OVER the UITableView, otherwise the loadingOverlay
					// sits *in* the scrolling area of the table
					this.View.Superview.Add (this.loadingOverlay); 
					this.View.Superview.BringSubviewToFront (this.loadingOverlay);
				}
				ConsoleD.WriteLine("Waiting for updates to finish before displaying table.");
			} else {
				this.loadingOverlay = null;
				if (this.AlwaysRefresh || this.Root == null || this.Root.Count == 0) {
					ConsoleD.WriteLine("Not updating, populating table.");
					this.PopulateTable();
				} else ConsoleD.WriteLine("Data already populated.");
			}
		}
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			PatientFileUpdateManager.UpdateFinished -= this.HandleUpdateFinished; 
		}
		void HandleUpdateFinished(object sender, EventArgs e)
		{
			ConsoleD.WriteLine("Updates finished, going to populate table.");
			this.InvokeOnMainThread ( () => {
				this.PopulateTable ();
				if (this.loadingOverlay != null)
					this.loadingOverlay.Hide ();
				this.loadingOverlay = null;
			});
		}
		
		protected virtual void PopulateTable()
		{
		}

		/// <summary>
		/// Whether the table will be reloaded on ViewWillAppear.
		/// </summary>
		protected bool AlwaysRefresh { get; set; }
	}
}