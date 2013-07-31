// -----------------------------------------------------------------------
// <copyright file="DataManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace core.net.tests.Data
{
    using System.Collections.Generic;

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
            The<IPatientDatabase>().WasToldTo(call => call.SavePatient(patient));
    }

    [Subject(typeof(DataManager), "Subject")]
    public class when_retrieving_settings : WithConcreteSubjectAndResult<DataManager, IDataManager, IEnumerable<Setting>>
    {
        Because of = () => Result = Subject.GetSettings();

        It should_call_patient_database_to_load_the_settings = () => 
            The<IPatientDatabase>().WasToldTo(call => call.GetItems<Setting>());
    }

    [Subject(typeof(DataManager), "settings")]
    public class when_saving_settings : WithConcreteSubject<DataManager, IDataManager>
    {
        static Setting setting;

        Establish context = () => setting = new Setting { Key = "Location.Coordinates", Value = "(12.00,-2.67)" };

        Because of = () => Subject.SaveSetting(setting);

        It should_call_the_patient_database_to_save_the_item = () => 
            The<IPatientDatabase>().WasToldTo(call => call.SaveItem(setting));
    }
}