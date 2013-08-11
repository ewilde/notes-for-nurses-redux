// -----------------------------------------------------------------------
// <copyright file="WindowsApplicationSettingsProvider.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using global::System.Configuration;

    using IApplicationSettingsProvider = Edward.Wilde.Note.For.Nurses.Core.Service.IApplicationSettingsProvider;

    public class WindowsApplicationSettingsProvider : IApplicationSettingsProvider
    {
        public string ReadValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}