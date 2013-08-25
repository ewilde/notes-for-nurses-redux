namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using TinyIoC;

    using PatientDetailViewController = Edward.Wilde.Note.For.Nurses.iOS.UI.iPhone.PatientDetailViewController;

    /// <summary>
	/// Patient element.
	/// on iPhone, pushes via MT.D
	/// on iPad, sends view to SplitViewController
	/// </summary>
	public class PatientTableElement : Element  
    {
        public IObjectFactory ObjectFactory { get; set; }

        static NSString cellId = new NSString ("PatientTableElement");
		Patient patient;

		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
		public PatientTableElement(IObjectFactory objectFactory, Patient showPatient) : this(objectFactory, showPatient, null)
		{
		}

        /// <summary>for iPad (SplitViewController)</summary>
        public PatientTableElement(IObjectFactory objectFactory, Patient showPatient, PatientSplitViewController patientSplitViewController)
            : base(showPatient.Name.ToString())
		{
			this.patient = showPatient;
			this.splitViewController = patientSplitViewController;
            this.ObjectFactory = objectFactory;
		}
		
		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (cellId);
			if (cell == null)
				cell = new PatientTableViewCell (UITableViewCellStyle.Subtitle, cellId, this.patient);
			else
				((PatientTableViewCell)cell).UpdateCell (this.patient);

			return cell;
		}

		/// <summary>Implement Monotouch dialog search on patient's display name</summary>
		public override bool Matches(string text)
		{
		    return this.patient.Name.DisplayName.IndexOf(text, System.StringComparison.CurrentCultureIgnoreCase) >= 0;
		}

		/// <summary>
		/// Behaves differently depending on iPhone or iPad
		/// </summary>
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			if (this.splitViewController != null)
			{
			    this.splitViewController.ShowPatient (this.patient.Id);
			}
			else 
            {
                var detailViewController = this.ObjectFactory.Create<PatientDetailViewController>(new NamedParameterOverloads { { "patientId", this.patient.Id } });
				detailViewController.Title = "Patient";
				dvc.ActivateController (detailViewController);
			}
		}
	}
}