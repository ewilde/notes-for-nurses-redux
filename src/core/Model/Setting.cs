// -----------------------------------------------------------------------
// <copyright file="Setting.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
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
        [SQLite.Unique]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}