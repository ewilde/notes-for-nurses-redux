// -----------------------------------------------------------------------
// <copyright file="DistanceCalculatorService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS.Services
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using MonoTouch.CoreLocation;

    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        public double DistanceBetween(LocationCoordinate locationA, LocationCoordinate locationB)
        {
            return new CLLocation(locationA.Latitude, locationA.Longitude)
                .DistanceFrom(new CLLocation(locationB.Latitude, locationB.Longitude));
        }
    }
}