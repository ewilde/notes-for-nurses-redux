// -----------------------------------------------------------------------
// <copyright file="Name.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// Represents a name of a person
    /// </summary>
    public class Name : BusinessEntityBase
    {
        [References(typeof(Patient))]
        [ForeignKey]
        [OnDeleteCascade]
        public int PatientId { get; set; }

        /// <summary>
        /// Gets or sets the persons first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [XmlAttribute]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [XmlAttribute]
        public string LastName { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [XmlIgnore]
        [SQLite.Ignore]
        public string DisplayName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}