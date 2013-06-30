// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationService.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    public class TypeRegistrationService : ITypeRegistrationService
    {
        public void RegisterAll()
        {
            TinyIoC.TinyIoCContainer.Current.Register<IFileManager, FileManager>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IDataManager, DataManager>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IPatientDatabase, PatientDatabase>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IPatientFileUpdateManager, PatientFileUpdateManager>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IPatientManager, PatientManager>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<ITypeRegistrationService>((container, overload) => this);
            TinyIoC.TinyIoCContainer.Current.Register<IObjectFactory, ObjectFactory>().AsSingleton();
            TinyIoC.TinyIoCContainer.Current.Register<IStartupManager, StartupManager>().AsSingleton();
        }
    }
}