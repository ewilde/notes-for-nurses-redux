// -----------------------------------------------------------------------
// <copyright file="SettingsFileWithEntries.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Service.Contexts
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    public class SettingsFileWithEntries
    {
        static IEnumerable<Setting> Entries;
        public SettingsFileWithEntries(IEnumerable<Setting> items)
        {
            Entries = items;
        }

        OnEstablish context = accessor =>
            {
                foreach (var setting in Entries)
                {
                    accessor.The<IApplicationSettingsProvider>().WhenToldTo(call => call.ReadValue(setting.Key)).Return(setting.StringValue);
                }                
            };
    }
}