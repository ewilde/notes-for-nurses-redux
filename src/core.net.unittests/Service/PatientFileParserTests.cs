// -----------------------------------------------------------------------
// <copyright file="PatientFileParserTests.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Service
{
    using System.IO;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    [Subject(typeof(PatientFileParser))]
    public class When_deserializing_the_seed_data : WithSubjectAndResult<PatientFileParser, PatientFile>
    {
        Because of = () => Result = Subject.Deserialize(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData\\SeedData.xml")));

        It should_hydrate_the_instance_and_contain_some_patients = () => Result.Patients.Count.ShouldBeGreaterThan(0);

        It should_hydrate_the_instance_patient_date_of_birth = () => Result.Patients[0].DateOfBirth.ShouldBeGreaterThan(DateTime.MinValue);

        It should_hydrate_the_instance_and_patient_name = () => Result.Patients[0].Name.ShouldNotBeNull();

        It should_hydate_the_instance_patient_first_name = () => Result.Patients[0].Name.FirstName.ShouldEqual("Sopoline");
    }
}