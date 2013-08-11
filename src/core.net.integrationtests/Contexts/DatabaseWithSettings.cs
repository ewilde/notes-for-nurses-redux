namespace core.net.integrationtests.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using Machine.Fakes;

    using core.net.integrationtests.Contexts;

    internal class DatabaseWithSettings : ContextBase
    {
        static IEnumerable<Setting> Settings;
        public DatabaseWithSettings(IEnumerable<Setting> settings)
        {
            Settings = settings;
        }

        OnEstablish context = engine =>
            {
                FakeAccessor = engine;
                ContextBase.Subject<SettingsManager>().Initialize();
                foreach (var setting in Settings)
                {
                    ContextBase.Subject<SettingsManager>().Save(setting);
                }
            };
    }
}