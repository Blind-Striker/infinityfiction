using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.StructConverters;

namespace CodeFiction.InfinityFiction.Core.StructContainer
{
    public class StructInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IGenericStructConverter>().ImplementedBy<GenericStructConverter>());
        }
    }
}
