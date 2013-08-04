namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class LocationCoordinateTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object result = null;
            string stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                result = new LocationCoordinate(stringValue);
            }
                
            return result ?? base.ConvertFrom(context, culture, value);
        }
    }
}