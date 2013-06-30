// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationService.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Service
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using TinyIoC;

    [Subject(typeof(TypeRegistrationService), "registration")]
    public class when_registration_service_has_completed : WithConcreteSubject<TypeRegistrationService, ITypeRegistrationService>
    {
        Because of = () => Subject.Register();

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
    }
}