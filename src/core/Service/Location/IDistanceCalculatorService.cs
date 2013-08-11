// -----------------------------------------------------------------------
// <copyright file="IDistanceCalculatorService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface IDistanceCalculatorService
    {
        /// <summary>
        /// Distances the between to location measured in meters.
        /// </summary>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <returns>The distance between <paramref name="coordinateA"/> - <paramref name="coordinateB"/>.</returns>
        double DistanceBetween(LocationCoordinate coordinateA, LocationCoordinate coordinateB);
    }
}