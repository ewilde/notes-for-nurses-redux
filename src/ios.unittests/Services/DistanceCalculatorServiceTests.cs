// -----------------------------------------------------------------------
// <copyright file="DistanceCalculatorServiceTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace ios.unittests.Services
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using NUnit.Framework;

    [TestFixture]
    public class DistanceCalculatorServiceTests : BaseTest
    {
        [Test]
        public void should_calculate_the_distance_correctly_between_two_points()
        {
            var london = new LocationCoordinate(51.500288, -0.126269);
            var paris = new LocationCoordinate(48.856495, 2.350907);

            var result = Resolve<IDistanceCalculatorService>().DistanceBetween(london, paris) / 1000;

            Assert.That(result, Is.AtLeast(342));
            Assert.That(result, Is.AtMost(343));
        }
    }
}