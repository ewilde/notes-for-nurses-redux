using System;
using Edward.Wilde.Note.For.Nurses.Core.DL.SQLite;

namespace Edward.Wilde.Note.For.Nurses.Core.BL.Contracts
{
    using Edward.Wilde.Note.For.Nurses.Core.DL.SQLite;

    /// <summary>
	/// Business entity base class. Provides the ID property.
	/// </summary>
	public abstract class BusinessEntityBase : IBusinessEntity
	{
		public BusinessEntityBase ()
		{
		}
		
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		/// <value>
		/// The ID.
		/// </value>
		[PrimaryKey, AutoIncrement]
		[System.Xml.Serialization.XmlIgnore]
        public int ID { get; set; }

	}
}

