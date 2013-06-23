using MonoTouch.UIKit;
using System.Drawing;
using System;
using System.Linq;
using MonoTouch.Foundation;
using MWC.iOS.Screens.Common;
using MWC.iOS.UI.Controls.Views;

namespace MWC.iOS.Screens.iPad.Speakers {
	public class PatientDetailViewController : UIViewController {
		UINavigationBar navBar;

		int speakerId;

	    SpeakerView speakerView;

		int colWidth1 = 335;
		int colWidth2 = 433;
	
		public UIPopoverController Popover;

		public PatientDetailViewController (int speakerID) //, UIViewController speakerView)
		{
			speakerId = speakerID;
			
			navBar = new UINavigationBar(new RectangleF(0,0,768, 44));
			navBar.SetItems(new UINavigationItem[]{new UINavigationItem("Speaker & Session Info")},false);
			
			View.BackgroundColor = UIColor.LightGray;
			View.Frame = new RectangleF(0,0,768,768);

			speakerView = new SpeakerView(-1);
			speakerView.Frame = new RectangleF(0,44,colWidth1,728);
			speakerView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

		
			View.AddSubview (speakerView);
			View.AddSubview (navBar);
		}

		public void Update(int speakerID) //, UIViewController view)
		{
			speakerId = speakerID;
			speakerView.Update (speakerID);
			speakerView.SetNeedsDisplay();
			

			if (Popover != null) {
				Popover.Dismiss (true);
			}
		}

	    public void AddNavBarButton (UIBarButtonItem button)
		{
			button.Title = "Speakers";
			navBar.TopItem.SetLeftBarButtonItem (button, false);
		}
		
		public void RemoveNavBarButton ()
		{
			navBar.TopItem.SetLeftBarButtonItem (null, false);
		}
	}
}