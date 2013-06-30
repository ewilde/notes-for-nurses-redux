// -----------------------------------------------------------------------
// <copyright file="StartupManagerTests.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    [Subject(typeof(StartupManager), "database")]
    public class When_the_startup_manager_is_run : WithSubject<StartupManager>
    {
        Because of = () => Subject.Run();

        It should_check_to_see_if_a_database_exists_already = () => The<IFileManager>().WasToldTo(message => message.Exists(Param.IsAny<string>()));        
    }    
}