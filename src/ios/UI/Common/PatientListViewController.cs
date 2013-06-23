namespace Edward.Wilde.Note.For.Nurses.iOS.UI.Common {
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.iOS.UI.iPad;
    using Edward.Wilde.Note.For.Nurses.iOS.Xamarin.Common;

    using MonoTouch.Dialog;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using Edward.Wilde.Note.For.Nurses.Core.BL;
    using Edward.Wilde.Note.For.Nurses.Core.BL.Managers;

    /// <summary>
	/// Speakers screen. Derives from MonoTouch.Dialog's DialogViewController to do 
	/// the heavy lifting for table population. Also uses ImageLoader in PatientTableViewCell.cs
	/// </summary>
	public partial class PatientListViewController : UpdateManagerLoadingDialogViewController {
		IList<Speaker> speakers;
		
		/// <summary>If this is null, on iPhone; otherwise on iPad</summary>
		PatientSplitViewController splitViewController;
		
		/// <summary>for iPhone</summary>
		public PatientListViewController () : this (null)
		{
		}
		/// <summary>for iPad</summary>
		public PatientListViewController (PatientSplitViewController patientSplitViewController) : base ()
		{
			this.splitViewController = patientSplitViewController;
			this.EnableSearch = true; // requires PatientTableElement to implement Matches()
		}
		
		/// <summary>
		/// Populates the page with exhibitors.
		/// </summary>
		protected override void PopulateTable()
		{
			this.speakers = SpeakerManager.GetSpeakers();

			this.Root = new RootElement ("Speakers") {
					from speaker in this.speakers
                    group speaker by (speaker.Index) into alpha
						orderby alpha.Key
						select new Section (alpha.Key) {
						from eachSpeaker in alpha
						   select (Element) new PatientTableElement (eachSpeaker, this.splitViewController)
			}};

            if (this.speakers.Count > 0)
            {
                // hide search until pull-down
                this.TableView.ScrollToRow (NSIndexPath.FromRowSection (0,0), UITableViewScrollPosition.Top, false);
                
            }
		}
		
		public override DialogViewController.Source CreateSizingSource (bool unevenRows)
		{
			return new PatientListTableSource(this, this.speakers);
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
		IList<Speaker> speakerList;
		public PatientListTableSource (DialogViewController dvc, IList<Speaker> speakers) : base(dvc)
		{
			this.speakerList = speakers;
		}

		public override string[] SectionIndexTitles (UITableView tableView)
		{
			var sit = from speaker in this.speakerList
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