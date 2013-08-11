// -----------------------------------------------------------------------
// <copyright file="Setting.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;
    using System.ComponentModel;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    /// <summary>
    /// An entity to store basic un-typed settings
    /// </summary>
    /// <example>
    /// <code>
    /// key: location.coordinates   value: (51.500288, -0.126269)
    /// </code>
    /// </example>
    public class Setting : BusinessEntityBase
    {
        private object value;

        [SQLite.Unique]
        public string Key { get; set; }

        [SQLite.Column("Value")]
        public string StringValue { get; set; }

        public TValue Value<TValue>()
        {
            if (this.value == null)
            {
                TypeConverter conv = TypeDescriptor.GetConverter(typeof(TValue));
                this.value = conv.ConvertFromString(this.StringValue);
            }

            return (TValue)this.value;
        }
    }
}