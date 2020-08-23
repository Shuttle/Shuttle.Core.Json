using Shuttle.Core.Container;
using Shuttle.Core.Contract;
using Shuttle.Core.Serialization;

namespace Shuttle.Core.Json
{
    public class Bootstrap : IComponentRegistryBootstrap
    {
        private static bool _registryBootstrapCalled;

        public void Register(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, nameof(registry));

            if (_registryBootstrapCalled)
            {
                return;
            }

            registry.AttemptRegister<ISerializer, JsonSerializer>();

            _registryBootstrapCalled = true;
        }
    }
}