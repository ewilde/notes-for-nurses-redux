using System.Collections.Generic;

namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    public class PatientFile
    {
        public PatientFile()
        {
            this.Patients = new List<Patient>();
        }

        public List<Patient> Patients { get; set; }
    }
}
