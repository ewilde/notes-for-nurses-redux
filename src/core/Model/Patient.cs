using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Edward.Wilde.Note.For.Nurses.Core.BL
{
    using Edward.Wilde.Note.For.Nurses.Core.DL.SQLite;

    public partial class Patient : Contracts.BusinessEntityBase
	{
		[XmlAttribute("k")]
		public string Key { get; set; }
		[XmlAttribute("n")]
		public string Name { get; set; }
		[XmlAttribute("t")]
		public string Title { get; set; }
		[XmlAttribute("c")]
		public string Company { get; set; }
		[XmlAttribute("b")]
		public string Bio { get; set; }
		[XmlAttribute("i")]
		public string ImageUrl { get; set; }
		
        public string Index {
            get {
                return Name.Length == 0 ? "A" : Name[0].ToString().ToUpper();
            }
        }
	}
}

