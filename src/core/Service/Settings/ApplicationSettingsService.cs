// -----------------------------------------------------------------------
// <copyright file="ApplicationSettingsService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System.ComponentModel;

    public class ApplicationSettingsService : IApplicationSettingsService
    {
        public IApplicationSettingsProvider SettingsProvider { get; set; }

        public ApplicationSettingsService(IApplicationSettingsProvider settingsProvider)
        {
            SettingsProvider = settingsProvider;
        }

        public string GetValue(string key)
        {
            return this.SettingsProvider.ReadValue(key);
        }

        public TValue GetValue<TValue>(string key)
        {
            TypeConverter conv = TypeDescriptor.GetConverter(typeof(TValue));
            return (TValue)conv.ConvertFromString(this.GetValue(key));
        }
    }
}