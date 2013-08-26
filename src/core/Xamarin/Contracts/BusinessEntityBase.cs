namespace Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts
{
    using System;

    using SQLite;

    /// <summary>
	/// Business entity base class. Provides the Id property.
	/// </summary>
	public abstract class BusinessEntityBase : IBusinessEntity
	{
        /// <summary>
		/// Gets or sets the Database Id.
		/// </summary>
		/// <value>
		/// The Id.
		/// </value>
		[PrimaryKey, AutoIncrement]
		[System.Xml.Serialization.XmlIgnore]
        public int Id { get; set; }

        public event EventHandler<EventArgs> ItemUpdated;

        public virtual void OnItemUpdated()
        {
            var handler = this.ItemUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
	}
}

