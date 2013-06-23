namespace Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Common {
    using System;
    using System.Diagnostics;
    using System.Drawing;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    using MonoTouch.Foundation;
    using MonoTouch.ObjCRuntime;
    using MonoTouch.UIKit;

    public delegate void FinishedFadeOutAndRemove ();

	public class LoadingDialogView : UIView {
		UILabel loadingMessageLabel;
		UIImageView overlayBackground;
		UIActivityIndicatorView activityIndicator;

		string message;
		bool initialized;

		public FinishedFadeOutAndRemove OnFinishedFadeOutAndRemove;
		
		public LoadingDialogView (string message, RectangleF bounds)
		{
			this.message = message;
			this.Initialize(message, bounds);
		}

		public LoadingDialogView (IntPtr handle) : base (handle)
		{
			this.message = "Loading...";
		}
 
		[Export("initWithCoder:")]
		public LoadingDialogView (NSCoder coder) : base (coder)
		{
			this.message = "Loading...";
		}
 
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			var b = this.Superview.Bounds; // defaults to 
			if (!this.initialized)
				this.Initialize (this.message, b);
		}

		void Initialize (string message, RectangleF bounds)
		{
			ConsoleD.WriteLine ("LoadingDialogView.Initialize " + bounds);
			this.SetUpLoadingMessageLabel (message);
			this.SetUpActivityIndicator ();
			this.SetUpOverlayBackground (bounds);
		
			this.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			this.AddSubview (this.overlayBackground);
			this.AddSubview (this.activityIndicator);
			this.AddSubview (this.loadingMessageLabel);

			this.initialized = true;
		}

		void SetUpOverlayBackground (RectangleF bounds)
		{
			this.overlayBackground = new UIImageView (bounds);
			this.overlayBackground.BackgroundColor = new UIColor (0f, 0f, 0f, 0.2f); // 0.75f
			//overlayBackground.BackgroundColor = UIColor.Blue;
			if (AppDelegate.IsPad)
				this.overlayBackground.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
		}


		void SetUpActivityIndicator ()
		{
			this.activityIndicator = new UIActivityIndicatorView (new RectangleF (150f, 220f, 20f, 20f));
			if (AppDelegate.IsPad)
				this.activityIndicator.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			this.activityIndicator.StartAnimating ();
		}
		
		void SetUpLoadingMessageLabel (string message)
		{
			// Set up loading message - Positioned Above centre in the middle
			this.loadingMessageLabel = new UILabel (new RectangleF(53f, 139f, 214f, 62f));
			this.loadingMessageLabel.BackgroundColor = UIColor.Clear;
			this.loadingMessageLabel.AdjustsFontSizeToFitWidth = true;
			this.loadingMessageLabel.TextColor = UIColor.White;
			this.loadingMessageLabel.TextAlignment = UITextAlignment.Center;
			this.loadingMessageLabel.Lines = 3;
			this.loadingMessageLabel.Text = message;
			this.loadingMessageLabel.Font = UIFont.BoldSystemFontOfSize (16f);
			if (AppDelegate.IsPad)
				this.loadingMessageLabel.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			this.AddSubview (this.loadingMessageLabel);
		}
		
		public override void WillRemoveSubview (UIView uiview)
		{
			if (this.activityIndicator != null)
				this.activityIndicator.StopAnimating ();
		}
		
		public void FadeOutAndRemove()
		{
			this.InvokeOnMainThread( delegate { 
				Debug.WriteLine ("Fade out loading screen...");
				UIView.BeginAnimations ("FadeOutLoadingView");
				UIView.SetAnimationDuration (0.5f);
				UIView.SetAnimationDelegate (this);
				UIView.SetAnimationTransition (UIViewAnimationTransition.None, this, true);
				UIView.SetAnimationDidStopSelector (new Selector ("FadeOutLoadingViewDone"));
			    this.Alpha = 0f;
				UIView.CommitAnimations();	
			});

		}
		
		[Export("FadeOutLoadingViewDone")]
		void FadeOutLoadingViewDone()
		{ 
			Debug.WriteLine ("RemoveFromSuperview...");
			this.RemoveFromSuperview ();
			this.OnFinishedFadeOutAndRemove ();
		}
	}
}