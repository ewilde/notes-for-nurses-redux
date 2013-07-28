// -----------------------------------------------------------------------
// <copyright file="PatientFileUpdateManager.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
#define TESTRUNNER


using System;

using Machine.Specifications;

namespace core.net.integrationtests.Data
{
    using System.IO;
    using System.Linq;

    using core.net.integrationtests.Contexts;
    using core.net.tests;

    using Edward.Wilde.Note.For.Nurses.Core.Data;

    [Subject(typeof(PatientFileUpdateManager))]
    public class when_loading_the_database_using_the_seed_data : WithConcreteSubject<PatientFileUpdateManager, IPatientFileUpdateManager>
    {
        static IDataManager DataManager;

        Establish context = () =>
            {
                With<EmptyDatabase>();
                DataManager = Resolve<IDataManager>();
                Configure(DataManager);
            };

        Because of = () =>
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\TestData", "SeedData.xml");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Seed data not found", path);
                }

                Subject.Update(File.ReadAllText(path));
            };

        It should_save_to_sql_lite = () =>
            {
                DataManager.GetPatients().Count().ShouldBeGreaterThan(0);
            };

        It should_save_the_patient_record = () =>
            {
                DataManager.GetPatients().ElementAt(0).ShouldNotBeNull();
            };

        It should_save_the_patient_name_record = () =>
            {
                DataManager.GetPatients().ElementAt(0).Name.ShouldNotBeNull();
            };

        It should_save_the_patient_name_first_name = () =>
            {
                DataManager.GetPatients().ElementAt(0).Name.FirstName.ShouldEqual("Sopoline");
            };
    }
}