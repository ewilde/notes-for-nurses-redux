// -----------------------------------------------------------------------
// <copyright file="DataManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace core.net.tests.Data
{
    using System.Collections.Generic;

    using core.net.tests.Data.Behaviors;

    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;
    using Machine.Specifications;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.tests;

    [Subject(typeof(DataManager), "saving")]
    public class When_saving_patient_entity : WithConcreteSubject<DataManager, IDataManager>
    {
        public static Patient Entity;
        
        Establish context = () =>
        {
            Entity = The<Patient>();
            With(new EntityThatFiresChangedEvent<DataManager>(Entity));
        };
                       
        Because of = () => Subject.SavePatient(Entity);

        It should_call_patient_database_to_save_the_item = () => 
            The<IPatientDatabase>().WasToldTo(call => call.SavePatient(Entity));

        It should_raise_the_item_updated_event =
            () => Entity.WasToldTo(call => call.OnItemUpdated());
    }

    [Subject(typeof(DataManager), "Subject")]
    public class when_retrieving_settings : WithConcreteSubjectAndResult<DataManager, IDataManager, IEnumerable<Setting>>
    {
        Because of = () => Result = Subject.GetSettings();

        It should_call_patient_database_to_load_the_settings = () => 
            The<IPatientDatabase>().WasToldTo(call => call.GetItems<Setting>());

        It should_call_application_settings_service_to_get_geofence_perimeter_size_in_kilometers = () =>
            The<IApplicationSettingsService>().WasToldTo(call => call.GetValue(SettingKey.GeofenceRadiusSizeInMeters.ToKeyString()));
    }

    [Subject(typeof(DataManager), "settings")]
    public class when_saving_settings : WithConcreteSubject<DataManager, IDataManager>
    {
        static Setting setting;

        Establish context = () => setting = new Setting { Key = "Location.Coordinates", StringValue = "(12.00,-2.67)" };

        Because of = () => Subject.SaveSetting(setting);

        It should_call_the_patient_database_to_save_the_item = () => 
            The<IPatientDatabase>().WasToldTo(call => call.SaveItem(setting));
    }
}