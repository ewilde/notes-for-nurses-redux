// -----------------------------------------------------------------------
// <copyright file="PatientFileUpdateManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace ios.unittests.Data
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using NUnit.Framework;

    [TestFixture]
    public class PatientFileUpdateManagerTests
    {
        public PatientFileUpdateManagerTests()
        {
            this.ObjectFactory = new ObjectFactory();
            this.FileManager = this.ObjectFactory.Create<IFileManager>();
            if (this.FileManager.Exists(PatientDatabase.DatabaseFilePath) && PatientDatabase.DebugMode)
            {
                this.FileManager.Delete(PatientDatabase.DatabaseFilePath);
            }

            this.PatientFileUpdateManager = this.ObjectFactory.Create<IPatientFileUpdateManager>();                        
        }

        [Test]
        public void Can_update_database_from_seed_data()
        {
            this.PatientFileUpdateManager.UpdateIfEmpty(async: false);    
        }

        public IObjectFactory ObjectFactory { get; set; }

        protected IPatientFileUpdateManager PatientFileUpdateManager { get; set; }

        protected IFileManager FileManager { get; set; }
    }
}