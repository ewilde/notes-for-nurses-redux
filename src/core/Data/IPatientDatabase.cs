namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    using SQLite;

    /// <summary>
    /// Patient database, responsible for retrieving and updating all entities in the application which
    /// are persisted in the <see cref="SQLiteConnection"/> database.
    /// </summary>
    public interface IPatientDatabase : IXamarinDatabase
    {
        /// <summary>
        /// Gets the Patient
        /// </summary>
        Patient GetPatient(int id);

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        void Close();

        /// <summary>
        /// Saves the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        void SavePatient(Patient patient);

        bool DataExists { get; }

        KnownCondition GetKnownCondition(int id);

        KnownCondition GetKnownConditionByName(string name);

        void DeleteAllData();
    }
}