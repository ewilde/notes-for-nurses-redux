namespace Edward.Wilde.Note.For.Nurses.Core.Xamarin.Contracts
{
    using System;

    public interface IBusinessEntity
	{
		int Id { get; set; }

        event EventHandler<EventArgs> ItemUpdated;

        void OnItemUpdated();
	}
}

