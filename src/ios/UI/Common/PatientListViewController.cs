namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;
    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Common;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using TinyIoC;

    /// <summary>
	/// Speakers screen. Derives from MonoTouch.Dialog's DialogViewController to do 
	/// the heavy lifting for table population. Also uses ImageLoader in PatientTableViewCell.cs
	/// </summary>
	public partial class PatientListViewController : UpdateManagerLoadingDialogViewController {
        public IPatientManager PatientManager { get; set; }

        public IObjectFactory ObjectFactory { get; set; }

        IList<Patient> patients;
		
		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		readonly PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
        public PatientListViewController(
            IPatientManager  patientManager,
            IPatientFileUpdateManager patientFileUpdateManager,
            IObjectFactory objectFactory) : this(patientManager, null, patientFileUpdateManager, objectFactory)
		{
		}

		/// <summary>for iPad</summary>
        public PatientListViewController(
            IPatientManager patientManager,
            PatientSplitViewController patientSplitViewController, 
            IPatientFileUpdateManager patientFileUpdateManager,
            IObjectFactory objectFactory)
            : base(patientFileUpdateManager)
		{
		    PatientManager = patientManager;
		    this.ObjectFactory = objectFactory;
		    this.splitViewController = patientSplitViewController;
			this.EnableSearch = true; // requires PatientTableElement to implement Matches()
		}
		
		/// <summary>
		/// Populates the page with exhibitors.
		/// </summary>
		protected override void PopulateTable()
		{
			this.patients = this.PatientManager.Get();

			this.Root = new RootElement("Patients") {
					from patient in this.patients
                    group patient by (patient.Index) into alpha
						orderby alpha.Key
						select 
                            new Section (alpha.Key) 
                            {
                                Elements = new List<Element>(
						            alpha.Select(
                                        eachSpeaker => (Element) 
                                            this.ObjectFactory.Create<PatientTableElement>(new NamedParameterOverloads { {"showPatient", eachSpeaker}, {"patientSplitViewController", this.splitViewController}})))
						    }};

            if (this.patients.Count > 0)
            {
                // hide search until pull-down
                this.TableView.ScrollToRow (NSIndexPath.FromRowSection (0,0), UITableViewScrollPosition.Top, false);
                
            }
		}
		
		public override DialogViewController.Source CreateSizingSource (bool unevenRows)
		{
			return new PatientListTableSource(this, this.patients);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }

	}
	
	/// <summary>
	/// Implement index
	/// </summary>
	public class PatientListTableSource : DialogViewController.SizingSource {
		IList<Patient> patientList;
		public PatientListTableSource (DialogViewController dvc, IList<Patient> patients) : base(dvc)
		{
			this.patientList = patients;
		}

		public override string[] SectionIndexTitles (UITableView tableView)
		{
			var sit = from speaker in this.patientList
                    group speaker by (speaker.Index) into alpha
						orderby alpha.Key
						select alpha.Key;
			return sit.ToArray();
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 60f;
		}
	}
}