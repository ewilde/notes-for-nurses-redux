using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;

    /// <summary>
    /// Responsible for managing the startup tasks that need to be performed when the application launches.
    /// </summary>
    public class StartupManager
    {
        public IFileManager FileManager { get; set; }

        public IPatientFileUpdateManager UpdateManager { get; set; }

        public StartupManager(IFileManager fileManager, IPatientFileUpdateManager updateManager)
        {
            FileManager = fileManager;
            UpdateManager = updateManager;
        }

        public void Run()
        {
            PatientDatabase.Initialize();
            if (this.FileManager.Exists(PatientDatabase.DatabaseFilePath) && PatientDatabase.DebugMode)
            {
                
            }
        }
    }
}
