using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
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

        public IPatientFileUpdateManager PatientFileUpdateManager { get; set; }

        public StartupManager(IFileManager fileManager, IObjectFactory objectFactory)
        {
            FileManager = fileManager;
            ObjectFactory = objectFactory;
        }

        public void Run()
        {
            if (this.FileManager.Exists(PatientDatabase.DatabaseFilePath) && PatientDatabase.DebugMode)
            {
                this.FileManager.Delete(PatientDatabase.DatabaseFilePath);
            }

            this.PatientFileUpdateManager = this.ObjectFactory.Create<IPatientFileUpdateManager>();
            this.PatientFileUpdateManager.UpdateIfEmpty();
        }
    }
}
