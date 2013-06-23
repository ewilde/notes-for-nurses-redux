using System;
using System.Collections.Generic;
using System.Linq;

namespace Edward.Wilde.Note.For.Nurses.Core.BL.Managers
{
	public static class SpeakerManager
	{
		static SpeakerManager ()
		{
		}

		internal static void UpdateSpeakerData(IList<Patient> speakers)
		{
			DAL.DataManager.DeleteSpeakers ();
			DAL.DataManager.SaveSpeakers (speakers); //SAL.MwcSiteParser.GetSpeakers ());			
		}
	
		public static IList<Patient> GetSpeakers ()
		{
            var ispeakers = DAL.DataManager.GetSpeakers();
            var speakers = ispeakers.ToList(); // new List<Patient>(ispeakers); //TODO: figure out Exception in Android
			speakers.Sort( (s1, s2) => s1.Name.CompareTo (s2.Name));
			return speakers;
		}
	
		public static Patient GetSpeaker (int speakerID)
		{
			return DAL.DataManager.GetSpeaker ( speakerID );
		}

        public static Patient GetSpeakerWithKey (string speakerKey)
        {
            return DAL.DataManager.GetSpeakerWithKey (speakerKey);
        }
	}
}

