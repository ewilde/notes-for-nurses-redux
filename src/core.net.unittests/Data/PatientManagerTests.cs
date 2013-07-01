// -----------------------------------------------------------------------
// <copyright file="PatientManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Data
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    [Subject(typeof(PatientManager), "saving")]
    public class When_saving_a_patient : WithConcreteSubject<PatientManager, IPatientManager>
    {
        static Patient patient;

        Establish context = () => patient = new Patient {Name = new Name { FirstName = "Ed", LastName = "Wilde" }};
        
        Because of = () => Subject.Save(patient);

        It should_call_the_data_manager_to_save_the_patient = () => 
            The<IDataManager>().WasToldTo(call => call.SavePatient(patient));
    }
}