// -----------------------------------------------------------------------
// <copyright file="LocationListInsideFence.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests.Service.Contexts
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    using core.net.tests.TestData;

    public class LocationListInsideFence
    {
        public static List<Location> NewLocations;
            
        OnEstablish context = engine =>
        {
            NewLocations = new List<Location> {Locations.JackTizard};

            // new location will be inside geofence
            engine
                .The<IDistanceCalculatorService>()
                .WhenToldTo(call=>call.DistanceBetween(Param<LocationCoordinate>.IsAnything, Param<LocationCoordinate>.IsAnything))
                .Return(-10);                    
        };
 
    }
}