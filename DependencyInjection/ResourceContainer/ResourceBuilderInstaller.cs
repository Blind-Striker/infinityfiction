using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CodeFiction.InfinityFiction.Core.ResourceBuilder;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;

namespace CodeFiction.InfinityFiction.Core.ResourceContainer
{
    public class ResourceBuilderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDelegateHelper>().ImplementedBy<DelegateHelper>(),
                Component.For<IResourceConverter>().ImplementedBy<ResourceConverter>(),
                Component.For<IKeyResourceBuilder>().ImplementedBy<KeyResourceBuilder>(),
                Component.For<IDlgResourceBuilder>().ImplementedBy<DlgResourceBuilder>());
        }
    }
}
