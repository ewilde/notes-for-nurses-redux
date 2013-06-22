using System;
using MonoTouch.UIKit;
using MWC.BL;
using MWC.iOS.UI.Controls.Views;

namespace MWC.iOS.Screens.iPad {
	/// <summary>
	/// Session popup for use on the HomeScreen (iPad only).
	/// </summary>
	public class SessionPopupScreen : UIViewController {
		Session session;
		SessionView sessionView;
		
		public SessionPopupScreen (Session session)
		{
			this.session = session;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			sessionView = new SessionView(this);
			sessionView.Frame = new System.Drawing.RectangleF(0,0,540,620);
			sessionView.Update(session);

			View.AddSubview (sessionView);
		}
		
		public void Dismiss(bool wasDirty)
		{
			DismissViewController (true, () => {});
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {	// only ever used on iPad, so we don't check 
            return true;
        }
	}
}