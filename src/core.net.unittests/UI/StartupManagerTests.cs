// -----------------------------------------------------------------------
// <copyright file="StartupManagerTests.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.UI
{
    using System.Diagnostics;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Service;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    [Subject(typeof(StartupManager), "database")]
    public class When_the_startup_manager_is_run_in_all_conditions : WithSubject<StartupManager>
    {
        Establish context = () =>
            {
                With<StartupManagerWithMockedDependants>();
                With<PatientFileUpdateManagerBehaviour>();
                With<ExistingSettings>();
                With<DeviceInsideGeofence>();
            };

        Because of = () => Subject.Run();

        It should_check_to_see_if_a_database_exists_already = () => The<IFileManager>().WasToldTo(message => message.Exists(Param.IsAny<string>()));

        It should_call_the_patient_update_file_manager_inorder_to_load_seed_data_if_the_database_is_empty = () =>
            The<IPatientFileUpdateManager>().WasToldTo(call => call.UpdateIfEmpty(Param<bool>.IsAnything));

        It should_initialize_the_settings_manager = () => 
            The<ISettingsManager>().WasToldTo(call => call.Initialize());

        It should_initialize_the_geofence_service_so_that_we_can_check_to_see_if_device_is_within_allowed_area = () =>
            {
                The<IGeofenceService>().WasToldTo(call => call.Initialize());
            };       
    }    

    public class When_the_startup_manager_is_run_and_no_settings_exist : WithSubject<StartupManager>
    {
        Establish context = () =>
            {
                With<StartupManagerWithMockedDependants>();
                With<PatientFileUpdateManagerBehaviour>();
                With<NoExistingSettings>();
                With<DeviceInsideGeofence>();
            };

        Because of = () => Subject.Run();

        It should_show_the_configuration_screen = () =>
            {
                The<IScreenController>().WasToldTo(call => call.ConfigurationStart());
            };

        It should_not_show_the_home_screen = () =>
        {
            The<IScreenController>().WasNotToldTo(call => call.ShowHomeScreen());
        };
    }    

    public class When_the_startup_manager_is_run_and_settings_exist : WithSubject<StartupManager>
    {
        Establish context = () =>
            {
                With<StartupManagerWithMockedDependants>();
                With<PatientFileUpdateManagerBehaviour>();
                With<ExistingSettings>();
                With<DeviceInsideGeofence>();
            };

        Because of = () => Subject.Run();

        It should_initialize_the_session_context = () =>
            {
                The<ISessionContext>().WasToldTo(call => call.Initialize());
            };

        It should_show_the_home_screen = () =>
            {
                The<IScreenController>().WasToldTo(call => call.ShowHomeScreen());
            };

        It should_not_show_the_configuration_screen = () =>
            {
                The<IScreenController>().WasNotToldTo(call => call.ConfigurationStart());
            };
    }

    public class when_the_startup_manager_is_run_and_the_device_is_outside_the_geofence : WithSubject<StartupManager>
    {
        Establish context = () =>
        {
            With<StartupManagerWithMockedDependants>();
            With<PatientFileUpdateManagerBehaviour>();
            With<ExistingSettings>();
            With<DeviceOutsideGeofence>();
        };

        Because of = () => Subject.Run();

        It should_show_the_exit_screen = () =>
            {
                The<IScreenController>().WasToldTo(call => call.ShowExitScreen(Param<string>.IsAnything));
            };

        It should_not_show_the_home_screen = () =>
        {
            The<IScreenController>().WasNotToldTo(call => call.ShowHomeScreen());
        };
    }

    public class when_the_startup_manager_is_run_and_the_device_is_inside_the_geofence : WithSubject<StartupManager>
    {
        Establish context = () =>
        {
            With<StartupManagerWithMockedDependants>();
            With<PatientFileUpdateManagerBehaviour>();
            With<ExistingSettings>();
            With<DeviceInsideGeofence>();
        };

        Because of = () => Subject.Run();

        It should_not_show_the_exit_screen = () =>
            {
                The<IScreenController>().WasNotToldTo(call => call.ShowExitScreen(Param<string>.IsAnything));
            };

        It should_show_the_home_screen = () =>
        {
            The<IScreenController>().WasToldTo(call => call.ShowHomeScreen());
        };
    }
}