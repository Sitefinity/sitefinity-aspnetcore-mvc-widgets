using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SearchResults
{
    internal class SearchResultsJsonConverter : JsonConverter<SearchResultDocumentDto>
    {
        public override SearchResultDocumentDto ReadJson(JsonReader reader, Type objectType, SearchResultDocumentDto existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            SearchResultDocumentDto searchDocument = new SearchResultDocumentDto();
            var searchDocumentProperties = searchDocument.GetType().GetProperties().ToDictionary(k => k.Name, p => p);

            try
            {
                JObject jsonObject = JObject.Load(reader);

                // exlude properties with @odata.type to keep the index fields collection clean.
                foreach (JProperty prop in jsonObject.Properties().Where(prop => !prop.Name.Contains("@odata.type", StringComparison.InvariantCulture)))
                {
                    if (!searchDocumentProperties.Keys.Contains(prop.Name))
                    {
                        searchDocument.IndexedFields.Add(prop.Name, this.TransformPropertyValue(prop));
                    }
                    else
                    {
                        searchDocumentProperties[prop.Name].SetValue(searchDocument, prop.Value.ToObject<string>());
                    }
                }

                return searchDocument;
            }
            catch (JsonReaderException ex)
            {
                Debug.WriteLine($"Error while converting the API resposne for search results {ex.Message}");
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, SearchResultDocumentDto value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This converter is for deserialization only.");
        }

        private object TransformPropertyValue(JProperty jproperty)
        {
            if (jproperty.Value.Type == JTokenType.Array)
            {
                List<string> arrayValues = new List<string>();
                foreach (var arrayelement in jproperty.Values())
                {
                    arrayValues.Add(arrayelement.Value<string>());
                }

                return arrayValues;
            }
            else
            {
                return jproperty.Value.ToString();
            }
        }
    }
}
