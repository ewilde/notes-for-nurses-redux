// -----------------------------------------------------------------------
// <copyright file="SettingsManagerTest.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Specifications;

namespace core.net.integrationtests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.integrationtests.Contexts;
    using core.net.tests;
    using core.net.tests.TestData;

    [Subject(typeof(SettingsManager), "saving")]
    public class When_saving_a_setting_to_the_patient_database : WithConcreteUnmockedSubjectAndResult<SettingsManager, ISettingsManager, IEnumerable<Setting>>
    {
        static Setting setting;

        static string Value;

        static string Key;

        static DateTime patientDateOfBirth = new DateTime(2001, 03, 23);

        Establish context = () =>
        {
            With<EmptyDatabase>();
            Key = "Location.Coordinates";
            Value = "(12.00,-2.67)";
            setting = new Setting { Key = Key, StringValue = Value };
        };

        Because of = () =>
        {
            Subject.Save(setting);
            Result = Subject.Get();
        };

        It should_give_the_item_an_id = () =>
            setting.Id.ShouldBeGreaterThan(0);

        It should_save_the_item_ = () =>
            Result.ShouldNotBeNull();

        It should_save_the_settings_key = () =>
            Result.ElementAt(0).Key.ShouldEqual(Key);

        It should_save_the_value = () =>
            Result.ElementAt(0).StringValue.ShouldEqual(Value);
    }

    [Subject(typeof(SettingsManager), "Retrieval")]
    public class when_getting_items_by_settings_key_and_the_key_has_already_been_saved : WithConcreteUnmockedSubjectAndResult<SettingsManager, ISettingsManager, LocationCoordinate>
    {
        private Establish context = () =>
            {
                With<EmptyDatabase>();
                With(new DatabaseWithSettings(
                        new[]
                            {
                                new Setting
                                    {
                                        Key = SettingKey.GeofenceLocation.ToKeyString(),
                                        StringValue = Locations.ParisCoordinate.ToString()
                                    }
                            }));
            };

        Because of = () => Result = Subject.Get<LocationCoordinate>(SettingKey.GeofenceLocation);

        It should_retrieve_the_underlying_value = () => Result.ShouldEqual(Locations.ParisCoordinate);
    }
}