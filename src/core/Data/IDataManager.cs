namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Defines an interface between the model and the data access layer.
    /// Should some entities be retrieved of persisted from a different datasource.
    /// </summary>
    public interface IDataManager
    {
        IEnumerable<Patient> GetPatients();

        Patient GetPatient(int id);

        void SavePatients (IEnumerable<Patient> items);

        int DeletePatient(int id);

        void DeletePatients();
        
        /// <summary>
        /// Gets a value indicating whether data already exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data exists otherwise, <c>false</c>.
        /// </value>
        bool DataExists { get; }
        
        /// <summary>
        /// Saves the patient entity. If it already exists it updates the entity; otherwise it inserts it.
        /// </summary>
        /// <param name="patient">The patient.</param>
        void SavePatient(Patient patient);

        /// <summary>
        /// Saves the setting. If it already exists it updates the entity; otherwise it inserts it.
        /// </summary>        
        /// <param name="setting">The setting.</param>
        void SaveSetting(Setting setting);

        /// <summary>
        /// Gets all of the settings.
        /// </summary>
        IEnumerable<Setting> GetSettings();
    }
}