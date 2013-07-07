// -----------------------------------------------------------------------
// <copyright file="Location.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public class Location
    {
        private static IDistanceCalculatorService distanceCalculator;

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
            if (Location.distanceCalculator == null)
            {
                Location.distanceCalculator = distanceCalculator;
            }
        }

        public static double operator -(Location location1, Location location2)
        {
            return distanceCalculator.DistanceBetween(location1.Coordinate, location2.Coordinate);
        }
    }
}