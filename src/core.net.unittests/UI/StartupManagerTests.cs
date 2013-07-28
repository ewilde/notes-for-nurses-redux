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
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    [Subject(typeof(StartupManager), "database")]
    public class When_the_startup_manager_is_run : WithSubject<StartupManager>
    {
        Establish context = () => 
            The<IObjectFactory>().WhenToldTo(call => call.Create<IPatientFileUpdateManager>()).Return(The<IPatientFileUpdateManager>);

        Because of = () => Subject.Run();

        It should_check_to_see_if_a_database_exists_already = () => The<IFileManager>().WasToldTo(message => message.Exists(Param.IsAny<string>()));

        It should_call_the_patient_update_file_manager_inorder_to_load_seed_data_if_the_database_is_empty = () =>
            The<IPatientFileUpdateManager>().WasToldTo(call => call.UpdateIfEmpty());
    }    
}