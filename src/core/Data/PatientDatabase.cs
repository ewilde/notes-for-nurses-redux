namespace Edward.Wilde.Note.For.Nurses.Core.Data 
{
    using System.Diagnostics;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    using SQLite;

    /// <summary>
    /// Patient database, responsible for retrieving and updating all entities in the application which
    /// are persisted in the <see cref="SQLiteConnection"/> database.
    /// </summary>
    public class PatientDatabase : XamarinDatabase, IPatientDatabase
    {
        /// <summary>
        /// The name of the database file
        /// </summary>
        public const string DatabaseFileName = "Patient.db3";

        public static new readonly string DatabaseFilePath = XamarinDatabase.GetDatabaseFilePath(PatientDatabase.DatabaseFileName);

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDatabase"/> class.
        /// If the database does not already exist it create a new database.
        /// </summary>
        public PatientDatabase() : base(GetDatabaseFilePath(DatabaseFileName))
        {
            this.SetForeignKeysPermissions(true);

			// create the tables
			this.CreateTable<Patient>();
            this.CreateTable<Name>();
		}

        /// <summary>
        /// Gets a value indicating whether we are in debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if in debug mode; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
		/// Gets the Patient represented by the specified <param name="id"></param>.
		/// </summary>
        public Patient GetPatient(int id)
        {
            lock (staticLock) 
            {
                Patient patient = this.Table<Patient>().FirstOrDefault(s => s.Id == id);
				return patient;
            }
        }
	}
}