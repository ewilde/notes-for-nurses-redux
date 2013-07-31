// -----------------------------------------------------------------------
// <copyright file="ISettingsManager.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    /// <summary>
    /// Defines the behaviour of the data access manager for storing and retrieval of settings
    /// </summary>
    public interface ISettingsManager
    {
        IEnumerable<Setting> Get();

        void Save(Setting value);
    }
}