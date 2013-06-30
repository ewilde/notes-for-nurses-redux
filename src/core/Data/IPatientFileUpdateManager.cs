namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface IPatientFileUpdateManager
    {
        event EventHandler UpdateStarted;

        event EventHandler UpdateFinished;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="PatientFileUpdateManager"/> is in the process of updating that database.
        /// </summary>
        /// <value>
        /// <c>true</c> if the data is being updating; otherwise, <c>false</c>.
        /// </value>
        bool UpdateInProgress { get; set; }

        void UpdateIfEmpty();

        /// <summary>
        /// Updates the database using the specified xml, which is deserialize into a <see cref="PatientFile"/> instance, first.
        /// </summary>
        /// <param name="xml">The xml representing a <see cref="PatientFile"/> object.</param>
        void Update(string xml);
    }
}