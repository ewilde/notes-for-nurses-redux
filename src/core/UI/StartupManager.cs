using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    public interface IStartupManager
    {
        void Run();
    }

    /// <summary>
    /// Responsible for managing the startup tasks that need to be performed when the application launches.
    /// </summary>
    public class StartupManager : IStartupManager
    {
        public IFileManager FileManager { get; set; }

        public IObjectFactory ObjectFactory { get; set; }

        public IScreenController ScreenController { get; set; }

        public ISettingsManager SettingsManager { get; set; }

        public IGeofenceService GeofenceService { get; set; }

        public ISessionContext SessionContext { get; set; }

        public IPatientFileUpdateManager PatientFileUpdateManager { get; set; }

        public StartupManager(
            IFileManager fileManager, 
            IObjectFactory objectFactory, 
            IScreenController screenController, 
            ISettingsManager settingsManager,
            IGeofenceService geofenceService,
            ISessionContext sessionContext)
        {
            FileManager = fileManager;
            ObjectFactory = objectFactory;
            ScreenController = screenController;
            SettingsManager = settingsManager;
            GeofenceService = geofenceService;
            SessionContext = sessionContext;
        }

        public void Run()
        {
            if (this.FileManager.Exists(PatientDatabase.DatabaseFilePath) && PatientDatabase.DebugMode)
            {
                this.FileManager.Delete(PatientDatabase.DatabaseFilePath);
            }

            this.PatientFileUpdateManager = this.ObjectFactory.Create<IPatientFileUpdateManager>();
            this.PatientFileUpdateManager.UpdateIfEmpty();

            this.SettingsManager.Initialize();
            if (!this.SettingsManager.DataExists)
            {
                this.ScreenController.ShowConfigurationScreen();
                return;
            }

            this.SessionContext.Initialize();

            if (!this.GeofenceService.Initialize())
            {
                this.ScreenController.ShowExitScreen("Your device is outside it's allowed home area.");
                return;
            }

            this.ScreenController.ShowHomeScreen();
        }
    }
}
