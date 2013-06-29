namespace Edward.Wilde.Note.For.Nurses.Core.Data 
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.DL;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.SAL;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    /// <summary>
    /// Used to manage saving the initial seed data file which is deserialized into a <see cref="PatientFile"/>
    /// and save to the database using <see cref="Update"/>.
    /// </summary>
    public static class PatientFileUpdateManager 
    {
        /// <summary>
        /// The global synchronization object, locks access across all threads in the application whilst updating.
        /// </summary>
		private static readonly object globalSync = new object();
		
		public static event EventHandler UpdateStarted = delegate {};

		public static event EventHandler UpdateFinished = delegate {};

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="PatientFileUpdateManager"/> is in the process of updating that database.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data is being updating; otherwise, <c>false</c>.
        /// </value>
        public static bool UpdateInProgress { get; set; }

        /// <summary>
        /// Gets a value indicating whether data already exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data exists; otherwise, <c>false</c>.
        /// </value>
        public static bool DataExists 
        {
			get 
            {
				return XamarinDatabase.CountTable<Patient>() > 0;
			}
		}

        /// <summary>
        /// Updates the database using the specified xml, which is deserialize into a <see cref="PatientFile"/> instance, first.
        /// </summary>
        /// <param name="xml">The xml representing a <see cref="PatientFile"/> object.</param>
		public static void Update(string xml)
		{
			ConsoleD.WriteLine ("Updating database using xml");

			lock (globalSync) 
            {
				UpdateInProgress = true;
				UpdateStarted (null, EventArgs.Empty);
                var finishedEventArgs = new UpdateFinishedEventArgs(UpdateType.SeedData, false);
				
                //TODO Inject when this is made an instance
				var patientFile = new PatientFileParser().Deserialize(xml);
				if (patientFile != null) 
                {
					if (SaveToDatabase(patientFile)) 
                    {
						finishedEventArgs.Success = true;
					}
				}

                UpdateFinished(null, finishedEventArgs);
				UpdateInProgress = false;
			}
		}
        
		private static bool SaveToDatabase(PatientFile patientFile)
		{
			bool result = false;
			try  
            {
                ConsoleD.WriteLine("Saving the patient file data to sqlite");
			
				if (patientFile.Patients.Count > 0) 
                {
					DataManager.DeletePatients ();
					DataManager.SavePatients (patientFile.Patients);
				}
				
				result = true;
			} 
            catch (Exception ex) 
            {
                ConsoleD.WriteError("Saving patient file data failed for the following reasons ", ex);
			}

			return result;
		}		
	}
}