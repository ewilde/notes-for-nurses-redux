// -----------------------------------------------------------------------
// <copyright file="DeviceOutsideGeofence.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    public class DeviceOutsideGeofence
    {
        OnEstablish context = engine =>
        {
            engine.The<IGeofenceService>()
                  .WhenToldTo(call => call.Initialize()).Return(false);

            engine.The<IGeofenceService>()
                  .WhenToldTo(call => call.InsidePerimeter()).Return(false);
                  
        };  
    }
}