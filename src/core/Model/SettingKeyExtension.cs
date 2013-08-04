namespace Edward.Wilde.Note.For.Nurses.Core.Model
{
    using System.Text;

    public static class SettingKeyExtension
    {
        public static string ToKeyString(this SettingKey value)
        {
            StringBuilder result = new StringBuilder();

            var keyString = value.ToString();
            foreach (var item in keyString)
            {
                if (char.IsUpper(item) && result.Length > 0 && result.Length < keyString.Length)
                {
                    result.Append('.');
                }

                result.Append(item);                
            }

            return result.ToString();
        }
    }
}