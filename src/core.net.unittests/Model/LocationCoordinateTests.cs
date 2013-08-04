// -----------------------------------------------------------------------
// <copyright file="LocationCoordinateTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Model
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using core.net.tests.TestData;

    [Subject(typeof(LocationCoordinate))]
    public class When_calling_to_string : WithResult<string>
    {
        static LocationCoordinate Subject;
        
        Establish context = () => Subject = Locations.LondonCoordinate;

        Because of = () => Result = Subject.ToString();

        It should_return_coordinates_in_standard_cartesian_string_representation = () => 
            Result.ShouldEqual(string.Format("({0},{1})", Subject.Latitude, Subject.Longitude));
    }

    public class When_creating_an_instance_using_cartesian_string_format : WithResult<LocationCoordinate>
    {
        Because of = () => Result = new LocationCoordinate(Locations.London.Coordinate.ToString());

        It should_parse_latitude = () => Result.Latitude.ShouldEqual(Locations.London.Coordinate.Latitude);
        
        It should_parse_longitude = () => Result.Longitude.ShouldEqual(Locations.London.Coordinate.Longitude);
    }

    [Subject(typeof(LocationCoordinate), "Equality")]
    public class when_comparing_equals_with_a_null_object : WithResult<bool>
    {
        static LocationCoordinate Subject;
        
        Establish context = () => Subject = Locations.LondonCoordinate;

        Because of = () => Result = Subject.Equals((object)null);

        It should_return_false = () => Result.ShouldBeFalse();
    }

    [Subject(typeof(LocationCoordinate), "Equality")]
    public class when_comparing_equals_with_an_object_of_a_different_type : WithResult<bool>
    {
        static LocationCoordinate Subject;
        
        Establish context = () => Subject = Locations.LondonCoordinate;

        Because of = () => Result = Subject.Equals("asdf");

        It should_return_false = () => Result.ShouldBeFalse();
    }

    [Subject(typeof(LocationCoordinate), "Equality")]
    public class when_comparing_equals_with_an_object_that_has_a_different_coordinate : WithResult<bool>
    {
        static LocationCoordinate Subject;
        
        Establish context = () => Subject = Locations.LondonCoordinate;

        Because of = () => Result = Subject.Equals(Locations.JackTizardCoordinate);

        It should_return_false = () => Result.ShouldBeFalse();
    }

    [Subject(typeof(LocationCoordinate), "Equality")]
    public class when_comparing_equals_with_an_object_that_has_the_same_coordinates : WithResult<bool>
    {
        static LocationCoordinate Subject;
        
        Establish context = () => Subject = Locations.LondonCoordinate;

        Because of = () => Result = Subject.Equals(new LocationCoordinate(Locations.LondonCoordinate.Latitude, Locations.LondonCoordinate.Longitude));

        It should_return_true = () => Result.ShouldBeTrue();
    }
}