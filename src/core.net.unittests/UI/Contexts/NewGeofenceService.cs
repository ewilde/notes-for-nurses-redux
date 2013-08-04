// -----------------------------------------------------------------------
// <copyright file="NewGeofenceService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI
{
    using System;
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    public class NewGeofenceService : ContextBase
    {
        OnEstablish context = engine =>
            {
                FakeAccessor = engine;
                FakeAccessor.Configure<ILocationListener>(new MockLocationListener());
        };  

        public static void LocationChanged(IEnumerable<Location> locations)
        {
            (FakeAccessor.The<ILocationListener>() as MockLocationListener).OnLocationChanged(new LocationChangedEventArgs(locations));
        }
    }

    public class MockLocationListener : ILocationListener
    {
        public bool EventSubscriptionCalled { get; private set; }

        private EventHandler<LocationChangedEventArgs> eventHandler;
        event EventHandler<LocationChangedEventArgs> ILocationListener.LocationChanged
        {
            add
            {
                EventSubscriptionCalled = true;
                eventHandler += value;
            }
            remove
            {
                var handler = this.eventHandler;
                if (handler != null)
                {
                    this.eventHandler -= value;
                }
            }
        }

        internal virtual void OnLocationChanged(LocationChangedEventArgs e)
        {
            var handler = eventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void StartListening(LocationSettings settings)
        {
            throw new NotImplementedException();
        }

        public void StopListening()
        {
            throw new NotImplementedException();
        }
    }
}
