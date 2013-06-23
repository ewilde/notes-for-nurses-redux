using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Edward.Wilde.Note.For.Nurses.Core.BL;

namespace Edward.Wilde.Note.For.Nurses.Core.SAL
{
    using Edward.Wilde.Note.For.Nurses.Core.BL;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class PatientFileParser
	{
		internal static PatientFile DeserializeConference (string xml)
		{
            PatientFile confData = null;
			try {
                var serializer = new XmlSerializer(typeof(PatientFile));
				var sr = new StringReader(xml);
                confData = (PatientFile)serializer.Deserialize(sr);
			} catch (Exception ex) {
				Debug.WriteLine ("ERROR deserializing patient file XML: " + ex);
			}
			return confData;
		}				
	}
}