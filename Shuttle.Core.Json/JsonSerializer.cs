using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Serialization;

namespace Shuttle.Core.Json
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonSerializer(IOptions<JsonSerializerOptions> jsonSerializeOptions)
        {
            Guard.AgainstNull(jsonSerializeOptions, nameof(jsonSerializeOptions));
            Guard.AgainstNull(jsonSerializeOptions.Value, nameof(jsonSerializeOptions.Value));

            _jsonSerializerOptions = jsonSerializeOptions.Value;
        }

        public Stream Serialize(object instance)
        {
            Guard.AgainstNull(instance, nameof(instance));

            var result = new MemoryStream();

            System.Text.Json.JsonSerializer.Serialize(result, instance, _jsonSerializerOptions);

            return result;
        }

        public object Deserialize(Type type, Stream stream)
        {
            Guard.AgainstNull(type, nameof(type));
            Guard.AgainstNull(stream, nameof(stream));

            return System.Text.Json.JsonSerializer.Deserialize(stream, type, _jsonSerializerOptions);
        }
    }
}