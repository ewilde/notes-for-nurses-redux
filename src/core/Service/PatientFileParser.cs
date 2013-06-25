namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    using Edward.Wilde.Note.For.Nurses.Core.Model;
    using Edward.Wilde.Note.For.Nurses.Core.Xamarin;

    /// <summary>
    /// Responsible for parsing the patient file from xml into a <see cref="PatientFile"/> instance.
    /// </summary>
    public class PatientFileParser
	{
        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>An instance of <see cref="PatientFile"/> represented by the passed in <paramref name="xml"/>.</returns>
        public PatientFile Deserialize(string xml)
		{
            PatientFile patientFile = null;
			try
			{
			    patientFile = xml.Deserialize<PatientFile>();
			} 
            catch (Exception ex) 
            {
				ConsoleD.WriteError("Error occured deserializing patient file data.", ex);
			}

			return patientFile;
		}				
	}
}