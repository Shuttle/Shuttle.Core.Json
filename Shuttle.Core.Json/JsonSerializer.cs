using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
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

            _jsonSerializerOptions = Guard.AgainstNull(jsonSerializeOptions.Value, nameof(jsonSerializeOptions.Value));
        }

        public Stream Serialize(object instance)
        {
            return SerializeAsync(instance, true).GetAwaiter().GetResult();
        }

        public object Deserialize(Type type, Stream stream)
        {
            Guard.AgainstNull(type, nameof(type));
            Guard.AgainstNull(stream, nameof(stream));

            return System.Text.Json.JsonSerializer.Deserialize(stream, type, _jsonSerializerOptions);
        }

        public async Task<Stream> SerializeAsync(object instance)
        {
            return await SerializeAsync(instance, false).ConfigureAwait(false);
        }

        private async Task<Stream> SerializeAsync(object instance, bool sync)
        {
            Guard.AgainstNull(instance, nameof(instance));

            var result = new MemoryStream();

            if (sync)
            {
                System.Text.Json.JsonSerializer.Serialize(result, instance, _jsonSerializerOptions);
            }
            else
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(result, instance, _jsonSerializerOptions).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<object> DeserializeAsync(Type type, Stream stream)
        {
            Guard.AgainstNull(type, nameof(type));
            Guard.AgainstNull(stream, nameof(stream));

            return await System.Text.Json.JsonSerializer.DeserializeAsync(stream, type, _jsonSerializerOptions);
        }

        public string Name => "Json";
    }
}