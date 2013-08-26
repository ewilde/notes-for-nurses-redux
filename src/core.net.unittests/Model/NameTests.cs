// -----------------------------------------------------------------------
// <copyright file="NameTests.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.Model
{
    using System.Collections.Generic;
    using System.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    
    [Subject(typeof(Name), "Comparing")]
    public class When_comparing_two_items_with_a_different_first_name : WithSubjectAndResult<IList<Name>, int>
    {
        Establish context = () =>
            {
                Subject = new List<Name>
                    {
                        new Name { FirstName = "Sarah", LastName = "Moon" },
                        new Name { FirstName = "Adam", LastName = "Ant" },
                    };
            };

        Because of = () => Result = Subject.ElementAt(0).CompareTo(Subject.ElementAt(1));

        It should_return_more_than_zero_for_a_name_alphabetically_after_the_item_being_compared = () => Result.ShouldEqual((int)CompareResult.MoreThan);
    }

    [Subject(typeof(Name), "Comparing")]
    public class When_comparing_two_items_with_a_equal_first_names_and_different_lastnames : WithSubjectAndResult<IList<Name>, int>
    {
        Establish context = () =>
            {
                Subject = new List<Name>
                    {
                        new Name { FirstName = "Adam", LastName = "Moon" },
                        new Name { FirstName = "Adam", LastName = "Ant" },
                    };
            };

        Because of = () => Result = Subject.ElementAt(0).CompareTo(Subject.ElementAt(1));

        It should_return_more_than_zero_for_a_name_alphabetically_after_the_item_being_compared = () => Result.ShouldEqual((int)CompareResult.MoreThan);
    }

    [Subject(typeof(Name), "Setting first and last name")]
    public class When_setting_the_first_and_last_name_from_a_display_name_with_two_words : WithSubject<Name>
    {
        Establish context = () => Subject = new Name();

        Because of = () => Subject.DisplayName = "Ben Hur";

        It should_set_the_first_name_taking_the_first_word = () => Subject.FirstName.ShouldEqual("Ben");

        It should_set_the_last_name_taking_the_second_word = () => Subject.LastName.ShouldEqual("Hur");
    }

    [Subject(typeof(Name), "Setting first and last name")]
    public class When_setting_the_first_and_last_name_from_a_display_name_with_one_words : WithSubject<Name>
    {
        Establish context = () => Subject = new Name();

        Because of = () => Subject.DisplayName = "Ben";

        It should_set_the_first_name_taking_the_first_word = () => Subject.FirstName.ShouldEqual("Ben");

        It should_set_the_last_name_to_string_empty = () => Subject.LastName.ShouldEqual(string.Empty);
    }

    [Subject(typeof(Name), "Setting first and last name")]
    public class When_setting_the_first_and_last_name_from_a_display_name_with_three_or_more_words : WithSubject<Name>
    {
        Establish context = () => Subject = new Name();

        Because of = () => Subject.DisplayName = "Ben James Erlend Oye";

        It should_set_the_first_name_taking_all_but_the_last_word = () => Subject.FirstName.ShouldEqual("Ben James Erlend");

        It should_set_the_last_name_to_string_empty = () => Subject.LastName.ShouldEqual("Oye");
    }
}