// -----------------------------------------------------------------------
// <copyright file="IGeofenceService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface IGeofenceService : IDisposable
    {
        bool Initialize();
        bool InsidePerimeter();
        LocationCoordinate CurrentLocation { get; set; }

        GeofenceState State { get; set; }

        LocationCoordinate CentreOfGeofence { get; }

        bool Timedout { get; }

        double RadiusOfGeofenceInMeters { get; }

        bool IsInitialized { get; }

        event EventHandler<EventArgs> OutsideFence;

        event EventHandler<EventArgs> InsideFence;
    }
}