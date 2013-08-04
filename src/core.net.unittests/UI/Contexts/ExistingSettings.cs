// -----------------------------------------------------------------------
// <copyright file="ExistingSettings.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;

    using Machine.Fakes;

    public class ExistingSettings
    {
        OnEstablish context = engine =>
        {
            engine.The<ISettingsManager>()
                  .WhenToldTo(call => call.DataExists)
                  .Return(true);
        }; 
    }
}