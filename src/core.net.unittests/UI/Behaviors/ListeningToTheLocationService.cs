// -----------------------------------------------------------------------
// <copyright file="ListeningToTheLocationService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.UI.Behaviors
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;
    using Machine.Specifications;

    [Behaviors]
    public class ListeningToTheLocationService<TSubject> : BehaviourBase<TSubject>
        where TSubject : class
    {
        It should_subscribe_to_the_location_changed_event = () =>
            {
                (Controller.The<ILocationListener>() as MockLocationListener).EventSubscriptionCalled.ShouldBeTrue();
            };
    }
}