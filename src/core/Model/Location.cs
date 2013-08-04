// -----------------------------------------------------------------------
// <copyright file="Location.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System.Diagnostics;

    using Edward.Wilde.Note.For.Nurses.Core.Service;

    public class Location
    {
        public static IDistanceCalculatorService DistanceCalculator;

        /// <summary>
        /// Gets or sets the direction. 0 through to 356 degrees.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public int Direction { get; set; }

        public LocationCoordinate Coordinate { get; set; }

        public Location(IDistanceCalculatorService distanceCalculator)
        {
            if (Location.DistanceCalculator == null)
            {
                Location.DistanceCalculator = distanceCalculator;
            }
        }

        public static double operator -(Location location1, Location location2)
        {
            return DistanceCalculator.DistanceBetween(location1.Coordinate, location2.Coordinate);
        }
    }
}