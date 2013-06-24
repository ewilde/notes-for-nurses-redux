using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Edward.Wilde.Note.For.Nurses.Core.BL.Managers {
    using Edward.Wilde.Note.For.Nurses.Core.DL;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.SAL;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    /// <summary>
	/// Central point for triggering data update from server
	/// to our local SQLite db
	/// </summary>
	public static class PatientFileUpdateManager {
		private static object @lock = new object();
		
		public static event EventHandler UpdateStarted = delegate {};
		public static event EventHandler UpdateFinished = delegate {};
		
		/// <summary>
		/// Gets or sets a value indicating whether the data is updating.
		/// </summary>
		/// <value>
		/// <c>true</c> if the data is updating; otherwise, <c>false</c>.
		/// </value>
		public static bool IsUpdating
		{
			get { return isUpdating; }
			set { isUpdating = value; }
		}

		private static bool isUpdating;
		
		public static bool HasDataAlready {
			get {
				return PatientDatabase.CountTable<Patient>() > 0;
			}
		}

		public static void UpdateFromFile(string xmlString)
		{
			ConsoleD.WriteLine ("### Updating all data from local file");

			// make this a critical section to ensure that access is serial
			lock (@lock) {
				isUpdating = true;
				UpdateStarted (null, EventArgs.Empty);
				var ea = new UpdateFinishedEventArgs (UpdateType.SeedData, false);
				
				var c = PatientFileParser.DeserializeConference (xmlString);
				if (c != null) {
					if (SaveToDatabase (c)) {
						ea.Success = true;
					}
				}
				UpdateFinished (null, ea);
				isUpdating = false;
			}
		}
        
		private static bool SaveToDatabase(PatientFile c)
		{
			bool success = false;
			try  {
                ConsoleD.WriteLine("yyy SAVING new conference data to sqlite");
			
				if (c.Speakers.Count > 0) {
					DAL.DataManager.DeleteSpeakers ();
					DAL.DataManager.SaveSpeakers (c.Speakers);
				}
				
				success = true;
			} catch (Exception ex) {
                ConsoleD.WriteLine("xxx SAVING conference to sqlite failed " + ex.Message);
			}
			return success;
		}		
	}
}