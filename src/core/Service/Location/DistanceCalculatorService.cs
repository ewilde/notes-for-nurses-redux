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
                new System.Device.Location.GeoCoordinate(coordinateA.Latitude, coordinateA.Longitude)
                    .GetDistanceTo(new GeoCoordinate(coordinateB.Latitude, coordinateB.Longitude));
        }
    }
}