// -----------------------------------------------------------------------
// <copyright file="PatientDatabaseTest.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.integrationtests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.integrationtests.Contexts;
    using core.net.tests;

    [Subject(typeof(PatientDatabase), "saving")]
    public class When_saving_a_patient_to_the_patient_database : WithConcreteSubjectAndResult<PatientDatabase, IPatientDatabase, Patient>
    {
        static Patient patient;

        static DateTime patientDateOfBirth = new DateTime(2001, 03, 23);

        Establish context = () =>
            {
                With<EmptyDatabase>();
                patient = new Patient
                    {
                        DateOfBirth = patientDateOfBirth,
                        Name = new Name { FirstName = "Bob", LastName = "Simons" },
                        KnownConditions =
                            {
                                new KnownCondition { Name = "Alergy" },
                                new KnownCondition { Name = "Seizure" }
                            }
                    };
            };

        Because of = () =>
            {
                Subject.SavePatient(patient);
                Result = Subject.GetPatient(patient.Id);
            };

        It should_give_the_item_an_id = () => 
            patient.Id.ShouldBeGreaterThan(0);

        It should_save_the_item_ = () =>
            Result.ShouldNotBeNull();

        It should_save_the_patients_date_of_birth = () =>
            Result.DateOfBirth.ShouldEqual(patientDateOfBirth);

        It should_save_the_patients_list_of_known_conditions = () =>
            Result.KnownConditions.Count.ShouldEqual(2);
    }

    [Subject(typeof(PatientDatabase), "saving")]
    public class When_saving_a_patient_to_the_patient_database_with_existing_store_conditions : WithConcreteSubjectAndResult<PatientDatabase, IPatientDatabase, Patient>
    {
        static Patient patient;

        static DateTime patientDateOfBirth = new DateTime(2001, 03, 23);

        Establish context = () =>
        {
            With<EmptyDatabase>();
            Subject.SaveItems(new[] { new KnownCondition { Name = "Alergy" }, new KnownCondition { Name = "Seizure" } });
            patient = new Patient
            {
                DateOfBirth = patientDateOfBirth,
                Name = new Name { FirstName = "Bob", LastName = "Simons" },
                KnownConditions =
                            {
                                new KnownCondition { Name = "Alergy" },
                                new KnownCondition { Name = "Seizure" }
                            }
            };
        };

        Because of = () =>
        {
            Subject.SavePatient(patient);
            Result = Subject.GetPatient(patient.Id);
        };

        It should_use_the_existing_conditions_in_the_link_table = () =>
            Subject.GetItems<KnownCondition>().Count().ShouldEqual(2);        
    }
}