// -----------------------------------------------------------------------
// <copyright file="DistanceCalculatorServiceTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Specifications;

    [Subject(typeof(DistanceCalculatorService), "distances")]
    public class when_calculating_the_distance_between_two_coordinates_say_london_and_paris : WithSubjectAndResult<DistanceCalculatorService, double>
    {
        private static LocationCoordinate london;

        private static LocationCoordinate paris;

        Establish context = () =>
            {
                london = new LocationCoordinate(51.500288, -0.126269);
                paris = new LocationCoordinate(48.856495, 2.350907);
            };

        Because of = () => Result = Subject.DistanceBetween(london, paris) / 1000;

        It should_return_about_343_kilometers = () => Result.ShouldBeCloseTo(343, 1);
    }
    [Subject(typeof(DistanceCalculatorService), "distances")]
    public class when_calculating_the_distance_between_two_coordinates_say_london_and_null : WithSubjectAndResult<DistanceCalculatorService, double>
    {
        private static LocationCoordinate london;

        private static LocationCoordinate null_coordinate;

        Establish context = () =>
            {
                london = new LocationCoordinate(51.500288, -0.126269);
                null_coordinate = null;
            };

        Because of = () => Result = Subject.DistanceBetween(london, null_coordinate) / 1000;

        It should_return_about_5731_kilometers = () => Result.ShouldBeCloseTo(5731, 1);
    }
}