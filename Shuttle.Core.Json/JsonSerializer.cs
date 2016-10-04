using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Json
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            Guard.AgainstNull(jsonSerializerSettings, "jsonSerializerSettings");

            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public Stream Serialize(object instance)
        {
            var result = new MemoryStream();
            using (var jsonWriter = new JsonTextWriter(new StreamWriter(result)) {CloseOutput = false})
            {
                var ser = Newtonsoft.Json.JsonSerializer.CreateDefault(_jsonSerializerSettings);
                ser.Serialize(jsonWriter, instance);
                jsonWriter.Flush();
                result.Position = 0;
                return result;
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            using (JsonTextReader jsonReader = new JsonTextReader(new StreamReader(stream)))
            {
                var ser = Newtonsoft.Json.JsonSerializer.CreateDefault(_jsonSerializerSettings);
                return ser.Deserialize(jsonReader, type);
            }
        }

        public static ISerializer Default()
        {
            return new JsonSerializer(new JsonSerializerSettings());
        }
    }
}