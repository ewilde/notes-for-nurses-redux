// -----------------------------------------------------------------------
// <copyright file="StringWriterWithEncoding.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// Creates a <see cref="StringWriter"/> that can have an encoding other than <see cref="System.Text.Encoding.Default"/>.
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        /// <summary>
        /// Encoding to use when the output is written.
        /// </summary>
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public StringWriterWithEncoding(StringBuilder stringBuilder, Encoding encoding) : base(stringBuilder)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.
        /// </summary>
        /// <returns>The Encoding in which the output is written.</returns>
        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}