using System;
using System.IO;
using Newtonsoft.Json;
using Shuttle.Core.Contract;
using Shuttle.Core.Serialization;

namespace Shuttle.Core.Json
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            Guard.AgainstNull(jsonSerializerSettings, nameof(jsonSerializerSettings));

            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public Stream Serialize(object instance)
        {
            var result = new MemoryStream();

            using (var jsonWriter = new JsonTextWriter(new StreamWriter(result)) {CloseOutput = false})
            {
                Newtonsoft.Json.JsonSerializer.CreateDefault(_jsonSerializerSettings).Serialize(jsonWriter, instance);
                jsonWriter.Flush();
                result.Position = 0;
            }

            return result;
        }

        public object Deserialize(Type type, Stream stream)
        {
            using (var jsonReader = new JsonTextReader(new StreamReader(stream)))
            {
                return Newtonsoft.Json.JsonSerializer
                    .CreateDefault(_jsonSerializerSettings)
                    .Deserialize(jsonReader, type);
            }
        }

        public static ISerializer Default()
        {
            return new JsonSerializer(new JsonSerializerSettings());
        }
    }
}