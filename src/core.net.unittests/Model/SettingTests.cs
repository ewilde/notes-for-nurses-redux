// -----------------------------------------------------------------------
// <copyright file="SettingTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Model
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using core.net.tests.TestData;

    [Subject(typeof(Setting), "Parsing values")]
    public class When_a_setting_is_a_location : WithSubjectAndResult<Setting, LocationCoordinate>
    {
        Establish context = () => Subject = new Setting { Key = SettingKey.GeofenceLocationCentre.ToKeyString(), StringValue = Locations.London.Coordinate.ToString()};

        Because of = () => Result = Subject.Value<LocationCoordinate>();

        It should_parse_the_underlying_value_and_return_a_location_coordinate = () => 
            Result.ShouldEqual(Locations.London.Coordinate);
    }

    [Subject(typeof(Setting), "Parsing values")]
    public class When_a_setting_is_a_string : WithSubjectAndResult<Setting, String>
    {
        Establish context = () => Subject = new Setting { Key = SettingKey.GeofenceLocationCentre.ToKeyString(), StringValue = "Bob"};

        Because of = () => Result = Subject.Value<string>();

        It should_parse_the_underlying_value_and_return_the_string = () => 
            Result.ShouldEqual("Bob");
    }


    [Subject(typeof(Setting), "Parsing values")]
    public class When_a_setting_is_an_integer : WithSubjectAndResult<Setting, int>
    {
        Establish context = () => Subject = new Setting { Key = SettingKey.GeofenceLocationCentre.ToKeyString(), StringValue = "909"};

        Because of = () => Result = Subject.Value<int>();

        It should_parse_the_underlying_value_and_return_the_integer = () => 
            Result.ShouldEqual(909);
    }
}