// -----------------------------------------------------------------------
// <copyright file="Locations.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.TestData
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes.Adapters.Moq;

    public static class Locations
    {
        public static LocationCoordinate LondonCoordinate = new LocationCoordinate(51.511226, -0.119833);

        public static LocationCoordinate JackTizardCoordinate = new LocationCoordinate(51.509724, -0.233621);

        public static LocationCoordinate ParisCoordinate = new LocationCoordinate(48.856654, 2.352216);

        public static readonly Location JackTizard = new Location(new Moq.Mock<IDistanceCalculatorService>().Object)
                    {
                        Coordinate = JackTizardCoordinate
                    };
        

        public static readonly Location Paris =
            new Location(new Moq.Mock<IDistanceCalculatorService>().Object)
                    {
                        Coordinate = ParisCoordinate
                    };

        public static readonly Location London = new Location(new Moq.Mock<IDistanceCalculatorService>().Object)
                    {
                        Coordinate = LondonCoordinate
                    };
    }
}