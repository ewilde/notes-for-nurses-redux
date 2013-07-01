namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Responsible for managing the patient entity persistance and retrieval.
    /// </summary>
    public class PatientManager : IPatientManager
    {
        public IDataManager DataManager { get; set; }

        public PatientManager(IDataManager dataManager)
        {
            DataManager = dataManager;
        }

        public void Update(IList<Patient> speakers)
        {
            this.DataManager.DeletePatients();
            this.DataManager.SavePatients(speakers);
		}
	
		public IList<Patient> Get() 
		{
            var patients = this.DataManager.GetPatients().OrderBy(x => x.Name);
		    return patients.ToList();
		}
	
		public Patient GetById(int id)
		{
            return this.DataManager.GetPatient(id);
		}

        public void Save(Patient patient)
        {
            this.DataManager.SavePatient(patient);
        }
    }
}

