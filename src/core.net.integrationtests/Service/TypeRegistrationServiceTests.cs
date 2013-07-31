// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationService.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace core.net.integrationtests.Service
{
    using System;

    using Machine.Specifications;

    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using TinyIoC;

    using core.net.tests;

    [Subject(typeof(TypeRegistrationService), "registration")]
    public class when_registration_service_has_completed : WithConcreteUnmockedSubject<TypeRegistrationService, ITypeRegistrationService>
    {
        Because of = () => Subject.RegisterAll();

        It should_be_possible_to_resolve_file_manager = () =>
            {
                TinyIoCContainer.Current.CanResolve<IFileManager>();
            };

        It should_have_registered_file_manager_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<IFileManager>(),
                    TinyIoCContainer.Current.Resolve<IFileManager>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_data_manager = () =>
            {
                TinyIoCContainer.Current.CanResolve<IDataManager>();
            };

        It should_have_registered_data_manager_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<IDataManager>(),
                    TinyIoCContainer.Current.Resolve<IDataManager>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_patient_database= () =>
            {
                TinyIoCContainer.Current.CanResolve<IPatientDatabase>();
            };

        It should_have_registered_patient_database_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<IPatientDatabase>(),
                    TinyIoCContainer.Current.Resolve<IPatientDatabase>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_patient_file_update_manager = () =>
            {
                TinyIoCContainer.Current.CanResolve<IPatientFileUpdateManager>();
            };

        It should_have_registered_patient_file_update_manager_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<IPatientFileUpdateManager>(),
                    TinyIoCContainer.Current.Resolve<IPatientFileUpdateManager>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_patient_manager = () =>
            {
                TinyIoCContainer.Current.CanResolve<IPatientManager>();
            };

        It should_have_registered_patient_manager_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<IPatientManager>(),
                    TinyIoCContainer.Current.Resolve<IPatientManager>()).ShouldBeTrue();
            };


        It should_be_possible_to_resolve_settings_manager = () =>
            {
                TinyIoCContainer.Current.CanResolve<ISettingsManager>();
            };

        It should_have_registered_settings_manager_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<ISettingsManager>(),
                    TinyIoCContainer.Current.Resolve<ISettingsManager>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_type_registration_service = () =>
            {
                TinyIoCContainer.Current.CanResolve<ITypeRegistrationService>();
            };

        It should_have_registered_type_registration_service_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<ITypeRegistrationService>(),
                    TinyIoCContainer.Current.Resolve<ITypeRegistrationService>()).ShouldBeTrue();
            };

        It should_be_possible_to_resolve_view_factory = () =>
            {
                TinyIoCContainer.Current.CanResolve<ITypeRegistrationService>();
            };

        It should_have_registered_type_view_factory_as_a_singleton = () =>
            {
                Object.ReferenceEquals(
                    TinyIoCContainer.Current.Resolve<ITypeRegistrationService>(),
                    TinyIoCContainer.Current.Resolve<ITypeRegistrationService>()).ShouldBeTrue();
            };
    }
}