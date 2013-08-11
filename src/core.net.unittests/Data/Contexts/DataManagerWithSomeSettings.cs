// -----------------------------------------------------------------------
// <copyright file="DataManagerWithSomeSettings.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Data.Contexts
{
    using System;
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    using Machine.Fakes;

    public class DataManagerWithSomeSettings : ContextBase
    {
        static IEnumerable<Setting> Settings;

        public DataManagerWithSomeSettings(IEnumerable<Setting> settings)
        {
            Settings = settings;
        }

        OnEstablish context = accessor =>
            {
                accessor.The<IDataManager>().WhenToldTo(call => call.GetSettings()).Return(Settings);
            };
    }
}