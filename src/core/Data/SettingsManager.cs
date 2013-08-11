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
            this.DataManager.SaveSetting(value);
            this.allSettings[value.Key] = value;
        }

        public bool DataExists
        {
            get
            {
                return false;
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