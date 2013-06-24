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
			DAL.DataManager.SaveSpeakers (speakers); //SAL.MwcSiteParser.GetPatients ());			
		}
	
		public static IList<Patient> GetPatients ()
		{
            var ispeakers = DAL.DataManager.GetSpeakers().OrderBy(x=> x.Name);
		    return ispeakers.ToList();
		}
	
		public static Patient GetPatient (int speakerID)
		{
			return DAL.DataManager.GetSpeaker ( speakerID );
		}        
	}
}

