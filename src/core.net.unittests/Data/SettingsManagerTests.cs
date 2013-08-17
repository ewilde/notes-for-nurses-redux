// -----------------------------------------------------------------------
// <copyright file="DataManagerTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace core.net.tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Machine.Fakes;
    using Machine.Specifications;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.tests;
    using core.net.tests.Data.Contexts;

    [Subject(typeof(SettingsManager), "initialization")]
    public class when_initializing_the_settings_manager : WithConcreteSubject<SettingsManager, ISettingsManager>
    {
        Establish context =
            () =>
            With(
                new DataManagerWithSomeSettings(
                    new[]
                        {
                            new Setting
                                {
                                    Key = SettingKey.GeofenceRadiusSizeInMeters.ToKeyString(),
                                    StringValue = "34"
                                }
                        }));
        Because of = () => Subject.Initialize();

        It should_retrieve_all_the_setting_from_the_data_manager = () =>
            The<IDataManager>().WasToldTo(call => call.GetSettings());

        It should_store_the_results_in_the_all_settings_property = () =>
            Subject.AllSettings.ElementAt(0).Value<int>().ShouldEqual(34);

    }

    public class when_saving_an_existing_setting : WithConcreteSubject<SettingsManager, ISettingsManager>
    {
        static int IdCalledWith, ExistingSettingId;

        Establish context =
            () =>
                {
                    IdCalledWith = 0;
                    ExistingSettingId = 202;
                    With(new DataManagerWithSomeSettings(new[]
                                                             {
                                                                 new Setting{ 
                                                                        Id = ExistingSettingId,
                                                                        Key = SettingKey.GeofenceRadiusSizeInMeters.ToKeyString(),  
                                                                        StringValue = "34"}
                                                             }));
                    Subject.Initialize();
                    The<IDataManager>().WhenToldTo(call => call.SaveSetting(Param<Setting>.IsAnything)).Callback<Setting>(setting=> IdCalledWith = setting.Id);
                };

        Because of = () => Subject.Save(new Setting { Key = SettingKey.GeofenceRadiusSizeInMeters.ToKeyString(), StringValue = "35"});

        It should_call_datamanager_using_the_id_of_the_existing_value = () => IdCalledWith.ShouldEqual(ExistingSettingId);
    }

    [Subject(typeof(SettingsManager), "loading")]
    public class When_retrieving_all_setting_entities : WithConcreteSubjectAndResult<SettingsManager, ISettingsManager, IEnumerable<Setting>>
    {
        Because of = () => Result = Subject.Get();

        It should_call_data_manager_to_retrieve_the_settings = () => 
            The<IDataManager>().WasToldTo(call => call.GetSettings());
    }

    [Subject(typeof(SettingsManager), "saving")]
    public class when_saving_a_setting : WithConcreteSubject<SettingsManager, ISettingsManager>
    {
        static Setting setting;

        Establish context = () => setting = new Setting { Key = "Location", StringValue = "London" };

        Because of = () => Subject.Save(setting);

        It should_call_the_data_manager_to_save_the_setting = () => The<IDataManager>().WasToldTo(call => call.SaveSetting(setting));
    }
}