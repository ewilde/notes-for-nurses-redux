// -----------------------------------------------------------------------
// <copyright file="ApplicationSettingsProvider.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace ios.unittests
{
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using NUnit.Framework;

    [TestFixture]
    public class ApplicationSettingsProviderTests : BaseTest
    {
        [Test]
        public void should_read_a_string_setting_from_the_plist_file()
        {
            var value = Resolve<IApplicationSettingsProvider>().ReadValue("CFBundleDisplayName");
            Assert.That(value, Is.EqualTo("notes4nurses-t"));
        }

        [Test]
        public void should_read_a_custom_setting_from_the_plist_file()
        {
            var value = Resolve<IApplicationSettingsProvider>().ReadValue("NotesForNursesPerimeterRadius");
            Assert.That(value, Is.EqualTo("8"));
        }
    }
}