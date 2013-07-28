using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using CodeFiction.InfinityFiction.Core.ServiceContracts;
using CodeFiction.InfinityFiction.Core.Services;
using CodeFiction.InfinityFiction.Core.Services.Key;

namespace CodeFiction.InfinityFiction.Core.ServiceContainer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IInfinityFictionConfigService>().ImplementedBy<InfinityFictionConfigService>().LifestyleSingleton(),
                Component.For<IKeyResourceService>().ImplementedBy<KeyResourceService>().LifestyleSingleton());
        }
    }
}
