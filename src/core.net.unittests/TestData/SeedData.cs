// -----------------------------------------------------------------------
// <copyright file="SeedData.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------


using System;

using Machine.Fakes;
using Machine.Specifications;

namespace core.net.tests.TestData
{
    using System.IO;
    using System.Xml.Linq;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Service;

    [Subject(typeof(string))]
    public class When_generating_seed_data : WithResult<string>
    {
        static XDocument names;        
        Establish context = () => names = XDocument.Parse(Names.ListOfNames);

        Because of = () =>
            {
                PatientFile patientFile = new PatientFile();
                int i = 1;
                foreach (var item in names.Descendants("Name"))
                {
                    patientFile.Patients.Add(
                        new Patient
                            {
                                DateOfBirth = new DateTime(Random(2000, 2013), Random(1, 12),  Random(1, 28)),
                                Name =
                                    new Name { FirstName = item.Value.Split(' ')[0], LastName = item.Value.Split(' ')[1] },
                                ProfilePicture = string.Format("Images/Profiles/picture({0})", i)

                            });

                    i = i + 1;
                }

                Result = patientFile.Serialize();
            };

        It should_write_out_a_new_file = () =>
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ios\images\SeedData.xml"), Result);
            };

        static Random  random = new Random();
        static int Random(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}