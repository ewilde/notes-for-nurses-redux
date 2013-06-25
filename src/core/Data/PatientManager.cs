namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public static class PatientManager
	{
        public static void UpdatePatients(IList<Patient> speakers)
        {
            DataManager.DeletePatients();
			DataManager.SavePatients (speakers);
		}
	
		public static IList<Patient> GetPatients() 
		{
            var patients = DataManager.GetPatients().OrderBy(x=> x.Name);
		    return patients.ToList();
		}
	
		public static Patient GetPatient(int id)
		{
			return DataManager.GetPatient(id);
		}        
	}
}

