using System;
using System.Collections.Generic;
using System.Linq;
using Edward.Wilde.Note.For.Nurses.Core.BL;

namespace Edward.Wilde.Note.For.Nurses.Core.DAL {
    using Edward.Wilde.Note.For.Nurses.Core.BL;

    /// <summary>
	/// [abstracts fromt the underlying data source(s)]
	/// [if multiple data sources, can agreggate/etc without BL knowing]
	/// [superflous if only one data source]
	/// </summary>
	public static class DataManager {
		#region Patient
		
		public static IEnumerable<Patient> GetSpeakers ()
		{
			return DL.MwcDatabase.GetItems<Patient> ();
		}
		
		public static Patient GetSpeaker (int id)
		{
			//return DL.MwcDatabase.GetItem<Patient> (id);
            return DL.MwcDatabase.GetSpeaker(id);
		}

        public static Patient GetSpeakerWithKey (string key)
        {
            return DL.MwcDatabase.GetSpeakerWithKey (key);
        }
		
		public static int SaveSpeaker (Patient item)
		{
			return DL.MwcDatabase.SaveItem<Patient> (item);
		}
		
		public static void SaveSpeakers (IEnumerable<Patient> items)
		{
			DL.MwcDatabase.SaveItems<Patient> (items);
		}
		
		public static int DeleteSpeaker(int id)
		{
			return DL.MwcDatabase.DeleteItem<Patient> (id);
		}
		
		public static void DeleteSpeakers()
		{
			DL.MwcDatabase.ClearTable<Patient>();
		}
		
		#endregion
	}
}