// -----------------------------------------------------------------------
// <copyright file="TypeRegistrationService.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public class TypeRegistrationService : ITypeRegistrationService
    {
        public void Register()
        {
            TinyIoC.TinyIoCContainer.Current.Register<IFileManager, FileManager>().AsSingleton();
        }
    }
}