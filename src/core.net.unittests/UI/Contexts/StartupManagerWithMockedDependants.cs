// -----------------------------------------------------------------------
// <copyright file="StartupManagerWithMockedDependants.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    using Machine.Fakes;

    public class StartupManagerWithMockedDependants
    {
        OnEstablish context = engine =>
        {
            engine.The<IObjectFactory>()
                .WhenToldTo(call => call.Create<IPatientFileUpdateManager>())
                .Return(engine.The<IPatientFileUpdateManager>());

            engine.The<IObjectFactory>()
                .WhenToldTo(call => call.Create<ISettingsManager>())
                .Return(engine.The<ISettingsManager>());

            engine.The<IObjectFactory>()
                .WhenToldTo(call => call.Create<ISessionContext>())
                .Return(engine.The<ISessionContext>());

            engine.The<IObjectFactory>()
                .WhenToldTo(call => call.Create<IGeofenceService>())
                .Return(engine.The<IGeofenceService>());
        }; 
    }
}