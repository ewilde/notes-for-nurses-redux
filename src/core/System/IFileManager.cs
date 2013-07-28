// -----------------------------------------------------------------------
// <copyright file="IFile.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    /// <summary>
    /// Provides methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>Determines whether the specified file exists.</summary>
        /// <returns>true if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, false. This method also returns false if <paramref name="path" /> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of <paramref name="path" />.</returns>
        /// <param name="path">The file to check. </param>
        bool Exists(string path);

        /// <summary>
        /// Gets the application resources directory.
        /// </summary>
        /// <value>
        /// The application resources directory.
        /// </value>
        string ResourcePath { get; }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">The path.</param>
        void Delete(string path);
    }
}