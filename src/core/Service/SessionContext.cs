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

        public void Initialize()
        {            
        }

        public LocationCoordinate GeofenceLocationCentre
        {
            get
            {
                return this.SettingsManager.Get<LocationCoordinate>(SettingKey.GeofenceLocationCentre);
            }
        }

        public int GeofenceRadiusSizeInMeters
        {
            get
            {
                return this.SettingsManager.Get<int>(SettingKey.GeofenceRadiusSizeInMeters);
            }
        }

        public SessionContext(ISettingsManager settingsManager)
        {
            this.SettingsManager = settingsManager;
        }        
    }
}