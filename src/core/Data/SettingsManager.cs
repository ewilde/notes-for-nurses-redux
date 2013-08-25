// -----------------------------------------------------------------------
// <copyright file="SettingsManager.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class SettingsManager : ISettingsManager
    {
        private Dictionary<string, Setting> allSettings;

        public IDataManager DataManager { get; set; }

        public SettingsManager(IDataManager dataManager)
        {
            this.DataManager = dataManager;
            this.allSettings = new Dictionary<string, Setting>();
        }

        public void Initialize()
        {
            foreach (var setting in this.Get())
            {
                this.allSettings.Add(setting.Key, setting);
            }            
        }

        public IEnumerable<Setting> Get()
        {
            return this.DataManager.GetSettings();
        }

        public void Save(Setting value)
        {
            if (allSettings.ContainsKey(value.Key))
            {
                this.allSettings[value.Key].StringValue = value.StringValue;
            }
            else
            {
                this.allSettings[value.Key] = value;
            }

            this.DataManager.SaveSetting(this.allSettings[value.Key]);
                
        }

        public bool DataExists
        {
            get
            {
                return this.allSettings.ContainsKey(SettingKey.GeofenceLocationCentre.ToKeyString());
            }
        }

        public IEnumerable<Setting> AllSettings
        {
            get
            {
                return this.allSettings.Values;
            }
        }

        public TValue Get<TValue>(SettingKey key)
        {
            var keyString = key.ToKeyString();
            
            Setting setting = null;
            if (this.allSettings.ContainsKey(keyString))
            {
                setting = this.allSettings[keyString];
            }

            if (setting == null)
            {
                return default(TValue);
            }

            return setting.Value<TValue>();
        }
    }
}