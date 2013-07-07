using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ios.unittests
{
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.iOS.Services;

    public class Application
    {
        static void Main(string[] args)
        {
            RegisterTypes();
            UIApplication.Main(args, null, "AppDelegate");
        }

        private static void RegisterTypes()
        {
            RegisterCrossPlatformTypes();
            RegisterSpecificPlatformTypes();
        }

        private static void RegisterCrossPlatformTypes()
        {
            new TypeRegistrationService().RegisterAll();
        }

        private static void RegisterSpecificPlatformTypes()
        {
            new AppleTypeRegistrationService().RegisterAll();
        }
    }
}