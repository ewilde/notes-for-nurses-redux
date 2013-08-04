// -----------------------------------------------------------------------
// <copyright file="ILocationListener.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;

    public interface ILocationListener
    {
        event EventHandler<LocationChangedEventArgs> LocationChanged;
 
        void StartListening(LocationSettings settings);

        void StopListening();        
    }
}