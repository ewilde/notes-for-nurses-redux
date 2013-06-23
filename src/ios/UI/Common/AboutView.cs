namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using System.Drawing;

    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    /// <summary>
	/// This screen REPLACES the old XIB version
	/// </summary>
	public class AboutView : UIViewController {
		protected string basedir;
		UIWebView webView;

		public AboutView ()
		{
			this.Title = "About Xamarin";
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.webView = new UIWebView();

			this.webView.ShouldStartLoad = 
			delegate (UIWebView webViewParam, NSUrlRequest request, UIWebViewNavigationType navigationType) {
				// view links in a new 'webbrowser' window like about, session & twitter
				if (navigationType == UIWebViewNavigationType.LinkClicked) {
					UIApplication.SharedApplication.OpenUrl (request.Url);
					return false;
				}
				return true;
			};

			this.Add (this.webView);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.webView.Frame = new RectangleF (0, 0, this.View.Bounds.Width, this.View.Bounds.Height);
			this.webView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			
			NSUrl url = null;
			
			if(AppDelegate.IsPad)
				url = NSUrl.FromFilename("Images/About/iPad/index.html");
			else
				url = NSUrl.FromFilename("Images/About/iPhone/index.html");
			var request = new NSUrlRequest(url);
			this.webView.LoadRequest(request);
		}
	}
}