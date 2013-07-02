namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Defines an interface between the model and the data access layer.
    /// Should some entities be retrieved of persisted from a different datasource.
    /// </summary>
    public class DataManager : IDataManager
    {
        public IPatientDatabase PatientDatabase { get; set; }

        public IFileManager FileManager { get; set; }

        public DataManager(IPatientDatabase patientDatabase, IFileManager fileManager)
        {
            PatientDatabase = patientDatabase;
            FileManager = fileManager;
        }

        public IEnumerable<Patient> GetPatients()
        {
            return this.PatientDatabase.GetItems<Patient>();
        }

        public Patient GetPatient(int id)
        {
            return this.PatientDatabase.GetPatient(id);
        }

        public void SavePatients(IEnumerable<Patient> items)
        {
            this.PatientDatabase.SaveItems<Patient>(items);
        }

        public int DeletePatient(int id)
        {
            return this.PatientDatabase.DeleteItem<Patient>(id);
        }

        public void DeletePatients()
        {
            this.PatientDatabase.ClearTable<Patient>();
        }

        /// <summary>
        /// Initializes this instance and all it's associated repository managers.
        /// </summary>
        public void Initialize()
        {
           
        }

        /// <summary>
        /// Gets a value indicating whether data already exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data exists otherwise, <c>false</c>.
        /// </value>
        public bool DataExists
        {
            get
            {
                return this.PatientDatabase.CountTable<Patient>() > 0;
            }
        }

        public void SavePatient(Patient patient)
        {
            this.PatientDatabase.SavePatient(patient);
        }
    }
}