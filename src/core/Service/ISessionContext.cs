// -----------------------------------------------------------------------
// <copyright file="ISessionContext.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface ISessionContext
    {
        void Initialize();

        LocationCoordinate GeofenceLocationCentre { get; }

        int GeofenceRadiusSizeInMeters { get; }
    }
}