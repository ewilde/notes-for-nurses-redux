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

        public SessionContext(ISettingsManager settingsManager)
        {
            this.SettingsManager = settingsManager;
        }

        public void Initialize()
        {
        }

        public LocationCoordinate GeofenceLocationCentre
        {
            get
            {
                return this.SettingsManager.Get<LocationCoordinate>(SettingKey.GeofenceLocationCentre);
            }
            set
            {
                this.SettingsManager.Save(
                   new Setting
                   {
                       Key = SettingKey.GeofenceLocationCentre.ToKeyString(),
                       StringValue = value.ToString()
                   });
            }
        }

        public int GeofenceRadiusSizeInMeters
        {
            get
            {
                return this.SettingsManager.Get<int>(SettingKey.GeofenceRadiusSizeInMeters);
            }
        }
    }
}