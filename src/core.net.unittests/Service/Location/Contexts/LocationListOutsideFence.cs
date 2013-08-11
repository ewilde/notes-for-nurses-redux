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

    public class LocationListOutsideFence
    {
        public static List<Location> Locations;
            
        OnEstablish context = engine =>
        {
            Locations = new List<Location> {TestData.Locations.Paris};

            // new location will be inside geofence
            engine
                .The<IDistanceCalculatorService>()
                .WhenToldTo(call=>call.DistanceBetween(Param<LocationCoordinate>.IsAnything, Param<LocationCoordinate>.IsAnything))
                .Return(1000);                    
        };
 
    }
}