// -----------------------------------------------------------------------
// <copyright file="FileManager.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using System.IO;

    /// <inheritdoc />
    public class FileManager : IFileManager
    {
        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}