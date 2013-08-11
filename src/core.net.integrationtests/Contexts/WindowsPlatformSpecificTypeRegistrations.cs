// -----------------------------------------------------------------------
// <copyright file="WindowsPlatformSpecificTypeRegistrations.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.integrationtests.Contexts
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;
    using Machine.Specifications;

    public class WindowsPlatformSpecificTypeRegistrations
    {
        OnEstablish context = engine =>
            {
                TinyIoC.TinyIoCContainer.Current.Register<ILocationListener, MockLocationListener>();
            }; 
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