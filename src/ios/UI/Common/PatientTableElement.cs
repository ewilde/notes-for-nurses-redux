namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using Edward.Wilde.Note.For.Nurses.Core.BL;

    using PatientDetailViewController = Edward.Wilde.Note.For.Nurses.iOS.UI.iPhone.PatientDetailViewController;

    /// <summary>
	/// Speaker element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class PatientTableElement : Element  {
		static NSString cellId = new NSString ("PatientTableElement");
		Speaker speaker;

		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
		public PatientTableElement (Speaker showSpeaker) : base (showSpeaker.Name)
		{
			this.speaker = showSpeaker;
		}
		/// <summary>for iPad (SplitViewController)</summary>
		public PatientTableElement (Speaker showSpeaker, PatientSplitViewController patientSplitViewController) : base (showSpeaker.Name)
		{
			this.speaker = showSpeaker;
			this.splitViewController = patientSplitViewController;
		}
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new PatientTableViewCell (UITableViewCellStyle.Subtitle, cellId, this.speaker);
			else
				((PatientTableViewCell)cell).UpdateCell (this.speaker);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return (this.speaker.Name + " " + this.speaker.Company).ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			if (this.splitViewController != null)
				this.splitViewController.ShowSpeaker (this.speaker.ID);
			else {
				var sds = new PatientDetailViewController (this.speaker.ID);
				sds.Title = "Speaker";
				dvc.ActivateController (sds);
			}
		}
	}
}