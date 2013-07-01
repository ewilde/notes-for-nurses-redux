namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Responsible for managing the patient entity persistance and retrieval.
    /// </summary>
    public interface IPatientManager
    {
        void Update(IList<Patient> speakers);

        IList<Patient> Get();

        Patient GetById(int id);

        void Save(Patient patient);
    }
}