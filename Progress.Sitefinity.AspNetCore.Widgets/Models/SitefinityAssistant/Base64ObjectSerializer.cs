using System;
using System.Text;
using System.Text.Json;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    internal static class Base64ObjectSerializer
    {
        public static string Serialize(object dto)
        {
            string json = JsonSerializer.Serialize(dto);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            string base64text = Convert.ToBase64String(jsonBytes);

            return base64text;
        }

        public static T Deserialize<T>(string serializedWidgetIdentifiers)
            where T : class
        {
            try
            {
                byte[] jsonBytes = Convert.FromBase64String(serializedWidgetIdentifiers);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                var dto = JsonSerializer.Deserialize<T>(jsonString);

                return dto;
            }
            catch
            {
                return null;
            }
        }
    }
}
