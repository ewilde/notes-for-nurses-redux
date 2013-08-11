// -----------------------------------------------------------------------
// <copyright file="DeviceInsideGeofence.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    public class DeviceInsideGeofence
    {
        OnEstablish context = engine =>
        {
            engine.The<IGeofenceService>()
                  .WhenToldTo(call => call.Initialize()).Return(true);

            engine.The<IGeofenceService>()
                  .WhenToldTo(call => call.InsidePerimeter()).Return(true);

        };

    }
}