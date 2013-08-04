namespace core.net.integrationtests.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using Machine.Fakes;

    internal class DatabaseWithSettings
    {
        static IEnumerable<Setting> Settings;
        public DatabaseWithSettings(IEnumerable<Setting> settings)
        {
            Settings = settings;
        }

        OnEstablish context = engine =>
            {
                foreach (var setting in Settings)
                {
                    engine.The<ISettingsManager>().Save(setting);
                }
            };
    }
}