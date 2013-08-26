// -----------------------------------------------------------------------
// <copyright file="EntityThatFiresChangedEvent.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Data.Behaviors
{
    using core.net.tests.UI;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts;

    using Machine.Fakes;
    using Machine.Specifications;

    public class EntityThatFiresChangedEvent<TSubject> : BehaviourBase<TSubject>
        where TSubject : class
    {
        private static IBusinessEntity Entity;

        public static bool ItemUpdatedEventRaised;

        public EntityThatFiresChangedEvent(IBusinessEntity entity)
        {
            Entity = entity;
            ItemUpdatedEventRaised = false;
        }

        OnEstablish context = accessor =>
        {
            Entity.ItemUpdated += (sender, args) => ItemUpdatedEventRaised = true;
        };
    }
}