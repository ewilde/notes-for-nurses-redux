using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Edward.Wilde.Note.For.Nurses.Core.BL
{
	public class Conference
	{
		public Conference ()
		{
			Speakers = new List<Speaker>();
			Sessions = new List<Session>();
			Exhibitors = new List<Exhibitor>();
		}

		[XmlElement("sp")]
		public List<Speaker> Speakers { get; set; }
		[XmlElement("se")]
		public List<Session> Sessions { get; set; }
		[XmlElement("ex")]
		public List<Exhibitor> Exhibitors { get; set; }
	}
}