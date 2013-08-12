namespace Edward.Wilde.Note.For.Nurses.Core.Data 
{
    using System;
    using System.IO;
    using System.Threading;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    /// <summary>
    /// Used to manage saving the initial seed data file which is deserialized into a <see cref="PatientFile"/>
    /// and save to the database using <see cref="Update"/>.
    /// </summary>
    public class PatientFileUpdateManager : IPatientFileUpdateManager
    {
        public IDataManager DataManager { get; set; }

        public IFileManager FileManager { get; set; }

        /// <summary>
        /// The global synchronization object, locks access across all threads in the application whilst updating.
        /// </summary>
		private static readonly object globalSync = new object();
		
		public event EventHandler UpdateStarted = delegate {};

		public event EventHandler UpdateFinished = delegate {};

        public PatientFileUpdateManager(IDataManager dataManager, IFileManager fileManager)
        {
            this.DataManager = dataManager;
            this.FileManager = fileManager;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="PatientFileUpdateManager"/> is in the process of updating that database.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data is being updating; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateInProgress { get; set; }

        public void UpdateIfEmpty(bool async = true)
        {
            Action updateAction = () =>
                {
                    bool dataExists = this.DataManager.DataExists;
                    ConsoleD.WriteLine("Database has seed data {0}.", dataExists);
                    if (!dataExists)
                    {
                        ConsoleD.WriteLine("Loading seed data");
                        var seedDataFile = this.FileManager.ResourcePath + "/Images/SeedData.xml";
                            // Note can't use Path.Combine as resource path point to your file app i.e. notes.app
                        string xml = System.IO.File.ReadAllText(seedDataFile);
                        this.Update(xml);
                    }
                };

            if (async)
            {
                new Thread(updateAction.Invoke).Start();
            }
            else
            {
                updateAction.Invoke();
            }
        }

        /// <summary>
        /// Updates the database using the specified xml, which is deserialize into a <see cref="PatientFile"/> instance, first.
        /// </summary>
        /// <param name="xml">The xml representing a <see cref="PatientFile"/> object.</param>
		public void Update(string xml)
		{
			ConsoleD.WriteLine ("Updating database using xml");

			lock (globalSync) 
            {
				this.UpdateInProgress = true;
                this.UpdateStarted(null, EventArgs.Empty);
                var finishedEventArgs = new UpdateFinishedEventArgs(UpdateType.SeedData, false);
				
                var patientFile = new PatientFileParser().Deserialize(xml);
				if (patientFile != null) 
                {
                    if (this.SaveToDatabase(patientFile)) 
                    {
						finishedEventArgs.Success = true;
					}
				}

                this.UpdateFinished(null, finishedEventArgs);
                this.UpdateInProgress = false;
			}
		}
        
		private bool SaveToDatabase(PatientFile patientFile)
		{
			bool result = false;
			try  
            {
                ConsoleD.WriteLine("Saving the patient file data to sqlite");
			
				if (patientFile.Patients.Count > 0) 
                {
					this.DataManager.DeletePatients ();
                    this.DataManager.SavePatients(patientFile.Patients);
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