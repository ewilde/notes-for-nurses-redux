// -----------------------------------------------------------------------
// <copyright file="ApplicationSettingsProvider.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS
{
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.Foundation;

    public class ApplicationSettingsProvider : IApplicationSettingsProvider
    {
        public string ReadValue(string key)
        {
            return NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString(key)).ToString();
        }
    }
}