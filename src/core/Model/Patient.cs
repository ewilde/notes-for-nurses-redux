namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// Represents a patient
    /// </summary>
    public class Patient : BusinessEntityBase
    {
        private Name name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
        {
            this.Name = new Name();
            this.KnownConditions = new List<KnownCondition>();
        }
        
        [XmlElement("Name")]
        [One2One(typeof(Name))]
        public Name Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [SQLite.Ignore]
        [XmlArray("KnownConditions"), XmlArrayItem("KnownCondition")]
        public List<KnownCondition> KnownConditions { get; private set; }

        [XmlAttribute("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [SQLite.Initialized]
        public bool Initialzied
        {
            get
            {
                return this.Name.PatientId > 0;
            }
        }

        public string Index
        {
            get
            {
                return this.Name == null || this.Name.FirstName.Length == 0 ? "A" : this.Name.FirstName[0].ToString().ToUpper();
            }
        }
    }
}

