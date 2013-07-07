// -----------------------------------------------------------------------
// <copyright file="ILocationService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;

    public interface ILocationService
    {
        event EventHandler<LocationChangedEventArgs> LocationChanged;
 
        void StartListening(LocationSettings settings);

        void StopListening();        
    }
}