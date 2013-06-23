using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MWC.BL;

namespace MWC.iOS.UI.CustomElements {
	/// <summary>
	/// Speaker element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class PatientTableElement : Element  {
		static NSString cellId = new NSString ("PatientTableElement");
		Speaker speaker;

		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		MWC.iOS.Screens.iPad.Speakers.PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
		public PatientTableElement (Speaker showSpeaker) : base (showSpeaker.Name)
		{
			speaker = showSpeaker;
		}
		/// <summary>for iPad (SplitViewController)</summary>
		public PatientTableElement (Speaker showSpeaker, MWC.iOS.Screens.iPad.Speakers.PatientSplitViewController patientSplitViewController) : base (showSpeaker.Name)
		{
			speaker = showSpeaker;
			this.splitViewController = patientSplitViewController;
		}
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new PatientTableViewCell (UITableViewCellStyle.Subtitle, cellId, speaker);
			else
				((PatientTableViewCell)cell).UpdateCell (speaker);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return (speaker.Name + " " + speaker.Company).ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			if (this.splitViewController != null)
				this.splitViewController.ShowSpeaker (speaker.ID);
			else {
				var sds = new MWC.iOS.Screens.iPhone.Speakers.PatientDetailViewController (speaker.ID);
				sds.Title = "Speaker";
				dvc.ActivateController (sds);
			}
		}
	}
}