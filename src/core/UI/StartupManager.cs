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
            IScreenController screenController)
        {
            FileManager = fileManager;
            ObjectFactory = objectFactory;
            ScreenController = screenController;
        }

        public void Run()
        {
            if (this.FileManager.Exists(PatientDatabase.DatabaseFilePath) && PatientDatabase.DebugMode)
            {
                this.FileManager.Delete(PatientDatabase.DatabaseFilePath);
            }

            // Create these objects after possibly deleting the database
            this.PatientFileUpdateManager = this.ObjectFactory.Create<IPatientFileUpdateManager>();
            this.PatientFileUpdateManager.UpdateIfEmpty();

            this.SettingsManager = this.ObjectFactory.Create<ISettingsManager>();
            this.SettingsManager.Initialize();
            if (!this.SettingsManager.DataExists)
            {
                this.ScreenController.StartConfiguration();
                return;
            }


            this.SessionContext = this.ObjectFactory.Create<ISessionContext>();
            this.SessionContext.Initialize();

            this.GeofenceService = ObjectFactory.Create<IGeofenceService>();
            if (!this.GeofenceService.Initialize())
            {
                this.ScreenController.ShowExitScreen("Your device is outside it's allowed home area.");
                return;
            }

            this.ScreenController.ShowHomeScreen();
        }
    }
}
