// -----------------------------------------------------------------------
// <copyright file="FileManager.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.iOS
{
    using System.IO;

    using MonoTouch.Foundation;

    public class FileManager : Core.IFileManager
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ResourcePath
        {
            get
            {
                return NSBundle.MainBundle.ResourcePath;
            }
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}