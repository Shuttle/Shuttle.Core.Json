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

            return services;
        }
    }
}