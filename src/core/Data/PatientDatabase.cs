namespace Edward.Wilde.Note.For.Nurses.Core.Data 
{
    using System.Diagnostics;

    using Edward.Wilde.Note.For.Nurses.Core.DL;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    
    /// <summary>
	/// <see cref="PatientDatabase"/> builds on SQLite.Net and represents a specific database, in our case, the Patient DB.
	/// It contains methods for retreival and persistance as well as db creation, all based on the underlying ORM.
	/// </summary>
	public class PatientDatabase : XamarinDatabase {

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDatabase"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        protected PatientDatabase (string path) : base (path)
        {
            this.SetForeignKeysPermissions(true);

			// create the tables
			this.CreateTable<Patient>();
            this.CreateTable<Name>();
		}

        public const string DatabaseFileName = "Patient.db3";

        public static bool DebugMode
        {
                
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif

            }
        }

        public static void Initialize()
        {
            string databaseFilePath = GetDatabaseFilePath(DatabaseFileName);
            ConsoleD.WriteLine("Patient database initializing, using path: " + databaseFilePath);
            database = new PatientDatabase(databaseFilePath);
        }

        /// <summary>
		/// Gets the Patient
		/// </summary>
        public static Patient GetPatient(int id)
        {
            lock (locker) {
                Patient patient = (from s in database.Table<Patient> ()
                        where s.Id == id
                        select s).FirstOrDefault ();				

				return patient;
            }
        }
		/// <summary>
		/// Gets the Patient
		/// </summary>
        public static Patient GetSpeakerWithKey (string key)
        {
            lock (locker) {
				Patient patient = (database.Table<Patient>().Where(s => s.Key == key)).FirstOrDefault ();

				
				return patient;
            }
        }
  
	}
}