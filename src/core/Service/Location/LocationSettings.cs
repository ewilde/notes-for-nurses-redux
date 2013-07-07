namespace Edward.Wilde.Note.For.Nurses.Core.Service
{
    public class LocationSettings
    {
        public LocationAccuracy DesiredAccuracy { get; set; }

        public bool IncludeHeading { get; set; }

        public string ReasonForListening { get; set; }
    }
}