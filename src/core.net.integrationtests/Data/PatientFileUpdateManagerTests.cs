﻿// -----------------------------------------------------------------------
// <copyright file="PatientFileUpdateManager.cs" company="UBS AG">
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

    using Edward.Wilde.Note.For.Nurses.Core.DL;
    using Edward.Wilde.Note.For.Nurses.Core.Data;

    [Subject(typeof(PatientFileUpdateManager))]
    public class when_loading_the_database_using_the_seed_data
    {        

        Establish context = () =>
            {
                string databaseFilePath = XamarinDatabase.GetDatabaseFilePath(PatientDatabase.DatabaseFileName);
                if (File.Exists(databaseFilePath))
                {
                    File.Delete(databaseFilePath);                        
                }

                PatientDatabase.Initialize();
            };

        Because of = () =>
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\TestData", "SeedData.xml");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Seed data not found", path);
                }

                PatientFileUpdateManager.Update(File.ReadAllText(path));
            };

        It should_save_to_sql_lite = () =>
            {
                DataManager.GetPatients().Count().ShouldBeGreaterThan(0);
            };
    }
}