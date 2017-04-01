using Shuttle.Core.Infrastructure;

namespace Shuttle.Core.Json
{
    public class Bootstrap : IComponentRegistryBootstrap
    {
        private static bool _registryBootstrapCalled;

        public void Register(IComponentRegistry registry)
        {
            Guard.AgainstNull(registry, "registry");

            if (_registryBootstrapCalled)
            {
                return;
            }

            registry.AttemptRegister<ISerializer, JsonSerializer>();

            _registryBootstrapCalled = true;
        }
    }
}