// -----------------------------------------------------------------------
// <copyright file="PatientKnownCondition.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using SQLite;

    /// <summary>
    /// This serves as a link entity, primary to satisfy the ORM. Links
    /// <see cref="Patient"/> to <see cref="KnownCondition"/>.
    /// </summary>
    public class PatientKnownCondition : BusinessEntityBase
    {
        [ForeignKey, References(typeof(Patient))]
        public int PatientId { get; set; }

        [ForeignKey, References(typeof(KnownCondition))]
        public int KnownConditionId { get; set; }
    }
}