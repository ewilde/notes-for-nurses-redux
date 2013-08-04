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
        public IDataManager DataManager { get; set; }

        public SettingsManager(IDataManager dataManager)
        {
            this.DataManager = dataManager;
        }

        public IEnumerable<Setting> Get()
        {
            return this.DataManager.GetSettings();
        }

        public void Save(Setting value)
        {
            this.DataManager.SaveSetting(value);
        }

        public bool DataExists
        {
            get
            {
                return false;
            }
        }

        public TValue Get<TValue>(SettingKey key) where TValue : class
        {
            var keyString = key.ToKeyString();
            Setting setting = this.Get().SingleOrDefault(item => item.Key.Equals(keyString));

            if (setting == null)
            {
                return default(TValue);
            }

            return setting.Value<TValue>();
        }
    }
}