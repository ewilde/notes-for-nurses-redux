// -----------------------------------------------------------------------
// <copyright file="IGeofenceService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface IGeofenceService
    {
        bool Initialize();
        bool InsidePerimeter(LocationCoordinate currentLocation, LocationCoordinate centreOfPerimeter);
    }
}