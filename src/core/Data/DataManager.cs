namespace Edward.Wilde.Note.For.Nurses.Core.Data {
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
	/// [abstracts fromt the underlying data source(s)]
	/// [if multiple data sources, can agreggate/etc without BL knowing]
	/// [superflous if only one data source]
	/// </summary>
	public static class DataManager 
    {
		
		public static IEnumerable<Patient> GetPatients()
		{
			return PatientDatabase.GetItems<Patient> ();
		}
		
		public static Patient GetPatient(int id)
		{
			//return DL.PatientDatabase.GetItem<Patient> (id);
            return PatientDatabase.GetPatient(id);
		}

        public static Patient GetSpeakerWithKey (string key)
        {
            return PatientDatabase.GetSpeakerWithKey (key);
        }
		
		public static int SaveSpeaker (Patient item)
		{
			return PatientDatabase.SaveItem<Patient> (item);
		}
		
		public static void SavePatients (IEnumerable<Patient> items)
		{
			PatientDatabase.SaveItems<Patient> (items);
		}
		
		public static int DeleteSpeaker(int id)
		{
			return PatientDatabase.DeleteItem<Patient> (id);
		}
		
		public static void DeletePatients()
		{
			PatientDatabase.ClearTable<Patient>();
		}		
	}
}