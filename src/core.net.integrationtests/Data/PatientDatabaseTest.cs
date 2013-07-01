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
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.integrationtests.Contexts;
    using core.net.tests;

    [Subject(typeof(PatientDatabase), "saving")]
    public class When_saving_a_patient_to_the_patient_database : WithConcreteSubjectAndResult<PatientDatabase, IPatientDatabase, Patient>
    {
        static Patient patient;
        
        Establish context = () =>
            {
                With<EmptyDatabase>();
                patient = new Patient { Name = new Name { FirstName = "Bob", LastName = "Simons" } };
            };

        Because of = () =>
            {
                Subject.SaveItem(patient);
                Result = Subject.GetPatient(patient.Id);
            };

        It should_give_the_item_an_id = () => 
            patient.Id.ShouldBeGreaterThan(0);

        It should_save_the_item_ = () =>
            Result.ShouldNotBeNull();
        
        It should_save_the_value_of_the_items_public_properties = () =>
            Result.ShouldNotBeNull();
            
    }
}