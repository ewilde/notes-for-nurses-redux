namespace core.net.integrationtests.Contexts
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Data;

    using Machine.Fakes;

    using core.net.tests.Util;

    public class EmptyDatabase : ContextBase
    {
        static readonly string databaseFilePath = XamarinDatabase.GetDatabaseFilePath(PatientDatabase.DatabaseFileName);
        static IPatientDatabase database;
        
        OnEstablish context = engine =>
            {
                database = Resolve<IPatientDatabase>();
                database.DeleteAllData();
                Debug.Assert(!database.DataExists);
            };      
    }
}