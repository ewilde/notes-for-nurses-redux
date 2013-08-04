// -----------------------------------------------------------------------
// <copyright file="SettingKeyExtentionTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Model
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    [Subject(typeof(SettingKeyExtension))]
    public class When_calling_to_key_string : WithResult<string>
    {
        Because of = () => Result = SettingKey.GeofenceLocation.ToKeyString();

        It should_insert_a_dot_before_each_capital_letter_unless_they_are_the_first_or_last_in_the_string = () =>
            Result.ShouldEqual("Geofence.Location");
    }
}