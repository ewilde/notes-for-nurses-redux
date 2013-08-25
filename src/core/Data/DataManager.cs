namespace Edward.Wilde.Note.For.Nurses.Core.Data
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    /// <summary>
    /// Defines an interface between the model and the data access layer.
    /// Should some entities be retrieved of persisted from a different datasource.
    /// </summary>
    public class DataManager : IDataManager
    {
        public IPatientDatabase PatientDatabase { get; set; }

        public IFileManager FileManager { get; set; }

        public IApplicationSettingsService ApplicationSettingsService { get; set; }

        public DataManager(IPatientDatabase patientDatabase, IFileManager fileManager, IApplicationSettingsService applicationSettingsService)
        {
            PatientDatabase = patientDatabase;
            FileManager = fileManager;
            ApplicationSettingsService = applicationSettingsService;
        }

        public IEnumerable<Patient> GetPatients()
        {
            return this.PatientDatabase.GetItems<Patient>();
        }

        public Patient GetPatient(int id)
        {
            return this.PatientDatabase.GetPatient(id);
        }

        public void SavePatients(IEnumerable<Patient> items)
        {
            this.PatientDatabase.SaveItems<Patient>(items);
        }

        public int DeletePatient(int id)
        {
            return this.PatientDatabase.DeleteItem<Patient>(id);
        }

        public void DeletePatients()
        {
            this.PatientDatabase.ClearTable<Patient>();
        }

        /// <summary>
        /// Initializes this instance and all it's associated repository managers.
        /// </summary>
        public void Initialize()
        {
           
        }

        /// <summary>
        /// Gets a value indicating whether data already exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data exists otherwise, <c>false</c>.
        /// </value>
        public bool DataExists
        {
            get
            {
                return this.PatientDatabase.DataExists;
            }
        }

        public void SavePatient(Patient patient)
        {
            this.PatientDatabase.SavePatient(patient);
        }

        public void SaveSetting(Setting setting)
        {
            this.PatientDatabase.SaveItem(setting);
        }

        public IEnumerable<Setting> GetSettings()
        {
            var list = new List<Setting>(this.PatientDatabase.GetItems<Setting>());
            list.AddRange(
                new[]
                    {
                        new Setting
                            {
                                Key = SettingKey.GeofenceRadiusSizeInMeters.ToKeyString(),
                                StringValue = this.ApplicationSettingsService.GetValue(SettingKey.GeofenceRadiusSizeInMeters.ToKeyString())
                            },
						new Setting
							{
								Key = SettingKey.GeofenceDefaultLocationCentre.ToKeyString(),
								StringValue = new LocationCoordinate(51.5010,0.1420).ToString()
							}
                    });

            return list;
        }
    }
}