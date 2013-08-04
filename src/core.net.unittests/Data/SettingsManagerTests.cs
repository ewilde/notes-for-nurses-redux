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