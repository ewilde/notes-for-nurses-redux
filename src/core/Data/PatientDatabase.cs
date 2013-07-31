namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;
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
        public PatientDatabase()
            : base(GetDatabaseFilePath(DatabaseFileName))
        {
            this.SetForeignKeysPermissions(true);

            // create the tables
            this.CreateTable<KnownCondition>();
            this.CreateTable<Patient>();
            this.CreateTable<PatientKnownCondition>();
            this.CreateTable<Name>();
            this.CreateTable<Setting>();
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

                // Look up known conditions stored for this patient in table PatientKnownCondition
                patient.KnownConditions.AddRange(
                    this.Table<PatientKnownCondition>()
                        .Where(condition => condition.PatientId == patient.Id)
                        .Select(condition => this.GetKnownCondition(condition.KnownConditionId)));

                return patient;
            }
        }

        public KnownCondition GetKnownConditionByName(string name)
        {
            return this.Table<KnownCondition>().FirstOrDefault(s => s.Name.Equals(name));
        }

        public void DeleteAllData()
        {
            this.DeleteAll<PatientKnownCondition>();
            this.DeleteAll<Patient>();
            this.DeleteAll<KnownCondition>();
            this.DeleteAll<Setting>();
        }

        public KnownCondition GetKnownCondition(int id)
        {
            return this.Table<KnownCondition>().FirstOrDefault(s => s.Id == id);
        }

        public void SavePatient(Patient patient)
        {
            lock (staticLock)
            {
                // update list of known conditions
                this.Table<PatientKnownCondition>()
                    .Where(x => x.PatientId == patient.Id)
                    .ForEach(x => this.Delete(x));

                this.SaveItem(patient);

                patient.KnownConditions
                       .ForEach(
                           condition =>
                           {
                               var existingCondition = this.GetKnownConditionByName(condition.Name);
                               if (existingCondition == null)
                               {
                                   this.SaveItem(condition);
                                   existingCondition = condition;
                               }

                               this.Insert(new PatientKnownCondition { PatientId = patient.Id, KnownConditionId = existingCondition.Id });
                           });

            }

        }

        public bool DataExists
        {
            get
            {
                return 
                    this.CountTable<Patient>() > 0 ||
                    this.CountTable<KnownCondition>() > 0 ||
                    this.CountTable<Setting>() > 0;
            }
        }
    }
}