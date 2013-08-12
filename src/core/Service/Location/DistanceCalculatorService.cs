// -----------------------------------------------------------------------
// <copyright file="DistanceCalculatorService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System.Device.Location;

    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        /// <summary>
        /// Distances between to coordinates.
        /// </summary>
        /// <param name="coordinateA">The first position.</param>
        /// <param name="coordinateB">The second position.</param>
        /// <returns>The distance in meters between <paramref name="coordinateA"/> and <paramref name="coordinateB"/>.</returns>
        public double DistanceBetween(LocationCoordinate coordinateA, LocationCoordinate coordinateB)
        {
            return
                new System.Device.Location.GeoCoordinate(coordinateA != null ? coordinateA.Latitude : 0, coordinateA != null ? coordinateA.Longitude : 0)
                .GetDistanceTo(new GeoCoordinate(coordinateB != null ? coordinateB.Latitude : 0, coordinateB != null ? coordinateB.Longitude : 0));
        }
    }
}