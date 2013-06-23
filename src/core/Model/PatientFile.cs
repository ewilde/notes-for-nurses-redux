using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.BL;

    public class PatientFile
    {
        [XmlElement("sp")]
        public List<Patient> Speakers { get; set; }

    }
}
