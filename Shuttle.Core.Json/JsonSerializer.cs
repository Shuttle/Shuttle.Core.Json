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
            return
                new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(instance, _jsonSerializerSettings)));
        }

        public object Deserialize(Type type, Stream stream)
        {
            return JsonConvert.DeserializeObject(new StreamReader(stream).ReadToEnd(), type, _jsonSerializerSettings);
        }

        public static ISerializer Default()
        {
            return new JsonSerializer(new JsonSerializerSettings());
        }
    }
}