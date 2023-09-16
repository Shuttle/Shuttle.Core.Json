using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Text.Json;
using Shuttle.Core.Serialization;

namespace Shuttle.Core.Json.Tests
{
    [TestFixture]
    public class JsonSerializerFixture
    {
        public class Data
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public Guid Guid { get; set; }
        }

        [Test]
        public void Should_be_able_to_serialize_and_deserialize()
        {
            ISerializer serializer = new JsonSerializer(Options.Create(new JsonSerializerOptions()));

            var data = new Data
            {
                Number = 123,
                Text = "123",
                Guid = Guid.NewGuid()
            };

            var stream = serializer.Serialize(data);

            stream.Position = 0;

            var deserialized = serializer.Deserialize<Data>(stream);

            Assert.That(deserialized.Number, Is.EqualTo(data.Number));
            Assert.That(deserialized.Text, Is.EqualTo(data.Text));
            Assert.That(deserialized.Guid, Is.EqualTo(data.Guid));
        }

        [Test]
        public async Task Should_be_able_to_serialize_and_deserialize_async()
        {
            ISerializer serializer = new JsonSerializer(Options.Create(new JsonSerializerOptions()));

            var data = new Data
            {
                Number = 123,
                Text = "123",
                Guid = Guid.NewGuid()
            };

            var stream = await serializer.SerializeAsync(data);

            stream.Position = 0;

            var deserialized = await serializer.DeserializeAsync<Data>(stream);

            Assert.That(deserialized.Number, Is.EqualTo(data.Number));
            Assert.That(deserialized.Text, Is.EqualTo(data.Text));
            Assert.That(deserialized.Guid, Is.EqualTo(data.Guid));
        }
    }
}