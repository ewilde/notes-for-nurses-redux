// -----------------------------------------------------------------------
// <copyright file="ApplicationSettingsProvider.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS
{
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.Foundation;

	using System;

    public class ApplicationSettingsProvider : IApplicationSettingsProvider
    {
        public string ReadValue(string key)
        {
			var keyObject = new NSString(key);

			if (NSBundle.MainBundle.InfoDictionary.ContainsKey(keyObject))
			{
				return NSBundle.MainBundle.InfoDictionary.ValueForKey(keyObject).ToString();
			}
            
			throw new Exception(string.Format("Key {0} not found", key));
        }
    }
}