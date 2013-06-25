namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using PatientDetailViewController = Edward.Wilde.Note.For.Nurses.iOS.UI.iPhone.PatientDetailViewController;

    /// <summary>
	/// Patient element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class PatientTableElement : Element  {
		static NSString cellId = new NSString ("PatientTableElement");
		Patient patient;

		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
		public PatientTableElement (Patient showPatient) : base (showPatient.Name.ToString())
		{
			this.patient = showPatient;
		}
		/// <summary>for iPad (SplitViewController)</summary>
		public PatientTableElement (Patient showPatient, PatientSplitViewController patientSplitViewController) : base (showPatient.Name.ToString())
		{
			this.patient = showPatient;
			this.splitViewController = patientSplitViewController;
		}
		
		static int count;
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			count++;
			if (cell == null)
				cell = new PatientTableViewCell (UITableViewCellStyle.Subtitle, cellId, this.patient);
			else
				((PatientTableViewCell)cell).UpdateCell (this.patient);

			return cell;
		}

		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return (this.patient.Name + " " + this.patient.Company).ToLower ().IndexOf (text.ToLower ()) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			if (this.splitViewController != null)
				this.splitViewController.ShowSpeaker (this.patient.Id);
			else {
				var sds = new PatientDetailViewController (this.patient.Id);
				sds.Title = "Patient";
				dvc.ActivateController (sds);
			}
		}
	}
}