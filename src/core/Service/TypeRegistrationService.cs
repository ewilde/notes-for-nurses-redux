// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationService.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using Edward.Wilde.Note.For.Nurses.Core.Data;
    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.UI;

    public class TypeRegistrationService : ITypeRegistrationService
    {
        public void RegisterAll()
        {
            var container = TinyIoC.TinyIoCContainer.Current;
            
           
            container.Register<IDataManager, DataManager>().AsSingleton();
            container.Register<IPatientDatabase, PatientDatabase>().AsSingleton();
            container.Register<IPatientFileUpdateManager, PatientFileUpdateManager>().AsSingleton();
            container.Register<IPatientManager, PatientManager>().AsSingleton();
            container.Register<ISettingsManager, SettingsManager>().AsSingleton();
            container.Register<IApplicationSettingsService, ApplicationSettingsService>().AsSingleton();
            container.Register<IGeofenceService, GeofenceService>().AsSingleton();
            container.Register<ISessionContext, SessionContext>().AsSingleton();
            container.Register<ITypeRegistrationService>((x, overload) => this);
            
            container.Register<IFileManager, FileManager>().AsSingleton();
            container.Register<IObjectFactory, ObjectFactory>().AsSingleton();
            
            container.Register<IStartupManager, StartupManager>().AsSingleton();
        }
    }
}