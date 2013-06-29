// -----------------------------------------------------------------------
// <copyright file="PatientTests.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

using Machine.Specifications;

namespace core.net.tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    using Machine.Fakes;

    [Subject(typeof(Patient))]
    public class when_a_patient_sort_order_is_being_determined : WithSubjectAndResult<Patient, string>
    {
        Establish context = () =>
            {
                Subject.Name = new Name { FirstName = "Alfred", LastName = "Jones" };
            };

        Because of = () => Result = Subject.Index;

        It should_calculate_the_index_using_the_first_letter_of_the_first_name = () => 
                                                                                 Result.ShouldEqual("A");
    }

    [Subject(typeof(Patient), "sorting")]
    public class when_sorting_patients_by_name : WithSubjectAndResult<List<Patient>, IEnumerable<Patient>>
    {

        Establish context = () =>
        {
            Subject = new List<Patient>
                    {
                        new Patient { Name = new Name { FirstName = "Mohammed", LastName = "Zayed" } },
                        new Patient { Name = new Name { FirstName = "Tony", LastName = "Benn" } },
                        new Patient { Name = new Name { FirstName = "Eleanor", LastName = "Rigby" } },
                    };
        };

        Because of = () =>
            {
                Result = Subject.OrderBy(x => x.Name);
            };

        It should_order_by_first_name = () =>
            {
                Result.ElementAt(0).Name.FirstName.ShouldEqual("Eleanor");
                Result.ElementAt(1).Name.FirstName.ShouldEqual("Mohammed");
                Result.ElementAt(2).Name.FirstName.ShouldEqual("Tony");
            };
    }

    [Subject(typeof(Patient))]
    public class when_serializing_a_patient_to_xml : WithSubjectAndResult<Patient, XDocument>
    {
        Establish context = () =>
            {
                Subject.Id = 8;
                Subject.Name = new Name { FirstName = "Rory", LastName = "Bremner" };
            };

        Because of = () => Result = XDocument.Parse(Subject.Serialize());

        It should_encode_to_utf_8 = () => Result.Declaration.Encoding.ShouldEqual("utf-8");

        It should_serialize_the_patient_element = () => Result.Root.Name.ShouldEqual("Patient");

        It should_not_include_the_id_property = () => Result.Root.Attribute("Id").ShouldBeNull();

        It should_serialize_the_patient_name_as_an_element = () => Result.Root.Descendants("Name").Single().ShouldNotBeNull();

        It should_serialize_the_patient_name_first_name_property = () => Result.Root.Descendants("Name").Single().Attribute("FirstName").Value.ShouldEqual("Rory");

        It should_serialize_the_patient_name_last_name_property = () => Result.Root.Descendants("Name").Single().Attribute("LastName").Value.ShouldEqual("Bremner");
    }
}