// -----------------------------------------------------------------------
// <copyright file="SessionContext.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class SessionContext : ISessionContext
    {
        public ISettingsManager SettingsManager { get; set; }

        public LocationCoordinate GeofenceLocation { get; set; }

        public SessionContext(ISettingsManager settingsManager)
        {
            this.SettingsManager = settingsManager;
        }

        public void Initialize()
        {
            this.GeofenceLocation = this.SettingsManager.Get<LocationCoordinate>(SettingKey.GeofenceLocation);
        }
    }
}