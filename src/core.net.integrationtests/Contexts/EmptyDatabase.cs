namespace core.net.integrationtests.Contexts
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    using Machine.Fakes;

    public class EmptyDatabase : ContextBase
    {
        OnEstablish context = engine =>
            {
                string databaseFilePath = XamarinDatabase.GetDatabaseFilePath(PatientDatabase.DatabaseFileName);
                if (File.Exists(databaseFilePath))
                {
                    File.Delete(databaseFilePath);
                }
            };

        OnCleanup shut_down_database_connection = x =>
            {
                Resolve<IPatientDatabase>().Close();
                Debug.WriteLine("Closed database connection.");
            };
    }
}