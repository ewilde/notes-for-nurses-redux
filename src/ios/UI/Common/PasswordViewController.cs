// -----------------------------------------------------------------------
// <copyright file="PasswordViewController.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
using MonoTouch.UIKit;
using System.Drawing;

namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common
{
    public class PasswordViewController : UIViewController
    {
		private const string kCPLCDefaultSetPrompt =			@"Enter your new passcode";
		private const string  kCPLCDefaultAuthPrompt =			@"Enter your passcode";

		private const string  kCPLCDefaultSetTitle =			@"Set Passcode";
		private const string  kCPLCDefaultConfirmTitle =		@"Confirm Passcode";
		private const string  kCPLCDefaultAuthTitle	= 		@"Enter Passcode";

		private const string  kCPLCDefaultSetError	=		@"Passcodes did not match. Try again.";
		private const string  kCPLCDefaultAuthError	=		@"Passcode incorrect. Try again.";

		UITextField hiddenField;
		UINavigationItem navigationItem;
		UILabel promptLabel;
		UILabel subPromptLabel;
		UITextField field1;
		UITextField field2;
		UITextField field3;
		UITextField field4;

		public string Prompt {
			get;
			set;
		}

		public string Passcode {
			get;
			set;
		}

		public string Title {
			get;
			set;
		}

		public PasswordControllerStyle Style {
			get;
			set;
		}

		public IPasswordControllerDelegate @Delegate {
			get;
			private set;
		}

		public PasswordViewController(IPasswordControllerDelegate @delegate)
		{
			this.Delegate = @delegate;
		}
         
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.SetupSubViews ();
		}

		private void SetupSubViews ()
		{
			this.View.BackgroundColor = UIColor.GroupTableViewBackgroundColor;

			if (AppDelegate.IsPad) {
				promptLabel = new UILabel (new RectangleF(0, 85, 540, 25));
			} else {
				promptLabel = new UILabel (new RectangleF(0, 85, 320, 25));
			}

			if (string.IsNullOrWhiteSpace (this.Prompt)) {
				if (this.Style == PasswordControllerStyle.Set) {
					this.Prompt = kCPLCDefaultSetPrompt;
				} else {
					this.Prompt = kCPLCDefaultAuthPrompt;
				}
			}
		}

		[System.Obsolete ("Deprecated in iOS6. Replace it with both GetSupportedInterfaceOrientations and PreferredInterfaceOrientationForPresentation")]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			if (AppDelegate.IsPad)
				return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
		}
    }

	public interface IPasswordControllerDelegate
	{
		void DidFinish (UIViewController controller, string passcode);
	}

	public enum PasswordControllerStyle
	{
		Set,
		Authenticate
	}
}