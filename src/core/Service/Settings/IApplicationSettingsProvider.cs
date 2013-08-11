// -----------------------------------------------------------------------
// <copyright file="IApplicationSettingsProvider.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    /// <summary>
    /// Defines behaviour to retrieve settings for an application for a particular platform.
    /// On windows the provider will look in the app.config, on ios the plist file.
    /// </summary>
    public interface IApplicationSettingsProvider
    {
        string ReadValue(string key);
    }
}