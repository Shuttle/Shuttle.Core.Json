using System;
using NUnit.Framework;

namespace Shuttle.Core.Json.Tests
{
    [TestFixture]
    public class JsonSerializerFixture
    {
        [Test]
        public void Should_be_able_to_serialize_and_deserialize()
        {
            var serializer = JsonSerializer.Default();

            var user = new User {Id = Guid.NewGuid(),Name = "user-name", DateActivated = DateTime.Now};

            var stream = serializer.Serialize(user);

            var deserialized = (User)serializer.Deserialize(typeof (User), stream);

            Assert.AreEqual(user.Id, deserialized.Id);
            Assert.AreEqual(user.Name, deserialized.Name);
            Assert.AreEqual(user.DateActivated, deserialized.DateActivated);
        }
    }
}