// -----------------------------------------------------------------------
// <copyright file="Name.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// Represents a name of a person
    /// </summary>
    public class Name : BusinessEntityBase, IComparable<Name>
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
        /// Compares the current object with another object of the same type.
        /// Orders alphabetically in ascending order by first name and then by lastname.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: 
        /// Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.
        /// Zero This object is equal to <paramref name="other" />. 
        /// Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(Name other)
        {
            if (other == null)
            {
                return (int)CompareResult.MoreThan;
            }

            var result = this.FirstName.CompareTo(other.FirstName);
            if (result == (int)CompareResult.Equal)
            {
                result = this.LastName.CompareTo(other.LastName);
            }

            return result;
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