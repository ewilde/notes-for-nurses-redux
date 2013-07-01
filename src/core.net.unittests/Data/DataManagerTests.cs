// -----------------------------------------------------------------------
// <copyright file="DataManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace core.net.tests.Data
{
    using Machine.Fakes;
    using Machine.Specifications;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.tests;

    [Subject(typeof(DataManager), "saving")]
    public class When_saving_patient_entity : WithConcreteSubject<DataManager, IDataManager>
    {
        static Patient patient;
        
        Establish context = () =>
            patient = new Patient { Name = new Name { FirstName = "Bob", LastName = "Moore" } };
                        

        Because of = () => Subject.SavePatient(patient);

        It should_call_patient_database_to_save_the_item = () => 
            The<IPatientDatabase>().WasToldTo(call => call.SaveItem(patient));
    }
}