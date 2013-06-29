// -----------------------------------------------------------------------
// <copyright file="NameTests.cs" company="UBS AG">
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
}