// -----------------------------------------------------------------------
// <copyright file="LocationCoordinate.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public struct LocationCoordinate
    {
        public LocationCoordinate(double latitude, double longitude) : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}