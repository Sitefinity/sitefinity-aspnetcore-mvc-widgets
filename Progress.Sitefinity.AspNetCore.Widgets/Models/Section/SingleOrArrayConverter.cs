using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.Section
{
    /// <summary>
    /// A JSON converter that handles deserialization of a JSON property that can either be a single object or an array of objects.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    public class SingleOrArrayConverter<T> : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<T>) || objectType == typeof(T);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>().FirstOrDefault();
            }
            else if (token.Type == JTokenType.Null)
            {
                return null;
            }

            // Single object, wrap in a list
            return token.ToObject<T>();
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
