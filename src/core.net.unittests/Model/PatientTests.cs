// -----------------------------------------------------------------------
// <copyright file="PatientTests.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

using Machine.Specifications;

namespace core.net.tests.Model
{
    using System.Linq;
    using System.Xml.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    [Subject(typeof(Patient))]
    public class when_a_patient_sort_order_is_being_determined : WithSubjectAndResult<Patient, string>
    {
        Establish context = () =>
            { 
                Subject.Name.FirstName = "Alfred";
                Subject.Name.LastName = "Jones";
            };

        Because of = () => Result = Subject.Index;

        It should_calculate_the_index_using_the_first_letter_of_the_first_name = () => 
            Result.ShouldEqual("A");
    }

    [Subject(typeof(Patient))]
    public class when_serializing_a_patient_to_xml : WithSubjectAndResult<Patient, XDocument>
    {
        Establish context = () =>
            {
                Subject.Id = 8;
                Subject.Name.FirstName = "Rory";
                Subject.Name.LastName = "Bremner";
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