using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Serialization;

namespace Shuttle.Core.Json
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonSerializer(this IServiceCollection services, Action<JsonSerializerBuilder> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var jsonSerializerBuilder = new JsonSerializerBuilder(services);

            builder?.Invoke(jsonSerializerBuilder);

            services.AddSingleton<ISerializer, JsonSerializer>();

            services.AddSingleton(Options.Create(jsonSerializerBuilder.Options));

            services.AddOptions<JsonSerializerOptions>().Configure(options =>
            {
                options.AllowTrailingCommas = jsonSerializerBuilder.Options.AllowTrailingCommas;
                options.DefaultBufferSize = jsonSerializerBuilder.Options.DefaultBufferSize;
                options.DefaultIgnoreCondition = jsonSerializerBuilder.Options.DefaultIgnoreCondition;
                options.DictionaryKeyPolicy = jsonSerializerBuilder.Options.DictionaryKeyPolicy;
                options.Encoder = jsonSerializerBuilder.Options.Encoder;
                options.IgnoreReadOnlyFields = jsonSerializerBuilder.Options.IgnoreReadOnlyFields;
                options.IncludeFields = jsonSerializerBuilder.Options.IncludeFields;
                options.IgnoreReadOnlyProperties = jsonSerializerBuilder.Options.IgnoreReadOnlyProperties;
                options.MaxDepth = jsonSerializerBuilder.Options.MaxDepth;
                options.NumberHandling = jsonSerializerBuilder.Options.NumberHandling;
                options.PropertyNameCaseInsensitive = jsonSerializerBuilder.Options.PropertyNameCaseInsensitive;
                options.PropertyNamingPolicy = jsonSerializerBuilder.Options.PropertyNamingPolicy;
                options.ReadCommentHandling = jsonSerializerBuilder.Options.ReadCommentHandling;
                options.ReferenceHandler = jsonSerializerBuilder.Options.ReferenceHandler;
                options.UnknownTypeHandling = jsonSerializerBuilder.Options.UnknownTypeHandling;
                options.WriteIndented = jsonSerializerBuilder.Options.WriteIndented;
            });

            return services;
        }
    }
}