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
        bool DataExists { get; }

        IEnumerable<Setting> AllSettings { get; }

        void Initialize();

        IEnumerable<Setting> Get();

        TValue Get<TValue>(SettingKey key);

        void Save(Setting value);
    }
}