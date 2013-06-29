namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Responsible for managing the patient entity persistance and retrieval.
    /// </summary>
    public static class PatientManager
	{
        public static void Update(IList<Patient> speakers)
        {
            DataManager.DeletePatients();
			DataManager.SavePatients (speakers);
		}
	
		public static IList<Patient> Get() 
		{
            var patients = DataManager.GetPatients().OrderBy(x=> x.Name);
		    return patients.ToList();
		}
	
		public static Patient GetById(int id)
		{
			return DataManager.GetPatient(id);
		}        
	}
}

