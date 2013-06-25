// -----------------------------------------------------------------------
// <copyright file="SerializerService.cs" company="UBS AG">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    public static class SerializerService
    {
        public static T Deserialize<T>(this string xml) where T : new()
        {
            T result = default(T);
            
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                var stringReader = new StringReader(xml);
                result = (T)serializer.Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                ConsoleD.WriteError("Deserializing type {0}.", ex, typeof(T).FullName);
            }

            return result;
        }

        public static string Serialize<T>(this T value)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriterWithEncoding(stringBuilder, Encoding.UTF8);            
            var xmlWriter = new XmlTextWriter(stringWriter)
                {
                    Formatting = Formatting.Indented
                };

            serializer.Serialize(xmlWriter, value);

            return stringBuilder.ToString();
        }
    }
}