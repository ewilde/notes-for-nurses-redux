// -----------------------------------------------------------------------
// <copyright file="PatientFileTests.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Model
{
    using Edward.Wilde.Note.For.Nurses.Core.Model;

    [Subject(typeof(PatientFile))]
    public class when_initializing_a_patient_file_instance : WithSubject<PatientFile>
    {
        It should_create_an_empty_list_of_patients = () =>
            {
                Subject.Patients.ShouldNotBeNull();
                Subject.Patients.Count.ShouldEqual(0);
            };
    }   
}