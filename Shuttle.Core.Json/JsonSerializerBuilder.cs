﻿using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Json
{
    public class JsonSerializerBuilder
    {
        public IServiceCollection Services { get; }

        public JsonSerializerOptions Options
        {
            get => _jsonSerializerOptions;
            set => _jsonSerializerOptions = value ?? throw new ArgumentNullException(nameof(value));
        }

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();

        public JsonSerializerBuilder(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            Services = services;
        }
    }
}