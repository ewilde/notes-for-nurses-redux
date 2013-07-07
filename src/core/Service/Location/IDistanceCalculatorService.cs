// -----------------------------------------------------------------------
// <copyright file="IDistanceCalculatorService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public interface IDistanceCalculatorService
    {
        double DistanceBetween(LocationCoordinate coordinateA, LocationCoordinate coordinateB);
    }
}