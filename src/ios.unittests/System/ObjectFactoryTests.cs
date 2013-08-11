// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationServiceTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace ios.unittests.Services
{
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using NUnit.Framework;

    [TestFixture]
    public class ObjectFactoryTests
    {
        [Test]
        public void Can_resolve_geofence_service()
        {
            var objectFactory = new ObjectFactory();
            var geofenceService = objectFactory.Create<IGeofenceService>();

            Assert.That(geofenceService, Is.Not.Null);
        }

        [Test]
        public void Can_resolve_startup_manager()
        {
            var objectFactory = new ObjectFactory();
            var manager = objectFactory.Create<IStartupManager>();

            Assert.That(manager, Is.Not.Null);
        }
    }
}