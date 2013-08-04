﻿// -----------------------------------------------------------------------
// <copyright file="IDistanceCalculatorService.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    public interface IDistanceCalculatorService
    {
        double DistanceBetween(LocationCoordinate coordinateA, LocationCoordinate coordinateB);
    }
}