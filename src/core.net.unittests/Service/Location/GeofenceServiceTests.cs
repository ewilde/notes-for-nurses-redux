// -----------------------------------------------------------------------
// <copyright file="GeofenceServiceTests.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Service
{
    using System.Collections.Generic;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using core.net.tests.Service.Contexts;
    using core.net.tests.UI;
    using core.net.tests.UI.Behaviors;

    [Subject(typeof(GeofenceService))]
    public class when_the_geofenceservice_is_constructed : WithConcreteSubject<GeofenceService, IGeofenceService>
    {
        Establish context = () => With<NewGeofenceService>();

        Because of = () => { };

        Behaves_like<ListeningToTheLocationService<GeofenceService>> it_has_subscribed_to_change_events = () => { };
    }

    [Subject(typeof(GeofenceService))]
    public class When_the_device_changes_location : WithConcreteSubject<GeofenceService, IGeofenceService>
    {
        Establish context = () =>
            {
                With<NewGeofenceService>();
                With<LocationListInsideFence>();
            };

        Because of = () => NewGeofenceService.LocationChanged(LocationListInsideFence.NewLocations);

        It should_call_the_distance_calcualtion_service_to_check_if_its_still_inside_the_fence = () => 
            The<IDistanceCalculatorService>().WasToldTo(call => call.DistanceBetween(Param<LocationCoordinate>.IsAnything, LocationListInsideFence.NewLocations[0].Coordinate));
    }

    [Subject(typeof(GeofenceService), "Subject")]
    public class when_initialize_is_called_and_device_recieves_location_update
    {
        Establish context = () => {  };

        Because of = () => { };

        It should_check_to_see_if_it_is_within_the_geofence = () => { };
    }
}