namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// Represents a patient
    /// </summary>
    public class Patient : BusinessEntityBase
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
        {
            //this.Name = new Name();
        }

        [XmlAttribute("k")]
		public string Key { get; set; }

        [One2One(typeof(Name))]
        public Name Name { get; set; }
		
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
                return this.Name == null || this.Name.FirstName.Length == 0 ? "A" : this.Name.FirstName[0].ToString().ToUpper();
            }
        }
	}
}

