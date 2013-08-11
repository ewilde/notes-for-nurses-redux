// -----------------------------------------------------------------------
// <copyright file="ApplicationSettingsServiceTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using core.net.tests.Service.Contexts;

    [Subject(typeof(ApplicationSettingsService), "Retrieval")]
    public class When_retrieving_an_item_from_the_settings_file : WithConcreteSubjectAndResult<ApplicationSettingsService, IApplicationSettingsService, int>
    {
        private Establish context =
            () =>
            With(
                new SettingsFileWithEntries(
                new[]
                    {
                        new Setting
                            {
                                Key = SettingKey.GeofenceRadiusSizeInMeters.ToKeyString(),
                                StringValue = "20"
                            }
                    }));

        Because of = () => Result = Subject.GetValue<int>(SettingKey.GeofenceRadiusSizeInMeters.ToKeyString());

        It should_call_the_platform_provider_to_read_in_the_value = () =>
            The<IApplicationSettingsProvider>().WasToldTo(call => call.ReadValue(SettingKey.GeofenceRadiusSizeInMeters.ToKeyString()));
    }
}