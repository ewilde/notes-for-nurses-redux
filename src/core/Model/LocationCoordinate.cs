// -----------------------------------------------------------------------
// <copyright file="LocationCoordinate.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocationCoordinateTypeConverter))]
    public class LocationCoordinate
    {
        public LocationCoordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public LocationCoordinate() : this(0, 0)
        {
        }

        public LocationCoordinate(string cartesianCoordinates) : this(0, 0)
        {
            var items = cartesianCoordinates.Split(new[] { ',' });
            this.Latitude = Convert.ToDouble(items[0].Substring(1));
            this.Longitude = Convert.ToDouble(items[1].Substring(0, items[1].Length - 1));
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.Latitude, this.Longitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is LocationCoordinate && Equals((LocationCoordinate)obj);
        }

        public bool Equals(LocationCoordinate other)
        {
            return this.Longitude.Equals(other.Longitude) && this.Latitude.Equals(other.Latitude);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Longitude.GetHashCode() * 397) ^ this.Latitude.GetHashCode();
            }
        }
    }
}