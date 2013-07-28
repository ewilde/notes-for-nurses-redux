// -----------------------------------------------------------------------
// <copyright file="FileManager.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using System;
    using System.IO;

    /// <inheritdoc />
    public class FileManager : IFileManager
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc />
        public string ResourcePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}