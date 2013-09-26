using Castle.MicroKernel.Registration;
using Castle.Windsor;

using CodeFiction.InfinityFiction.Core.Container;
using CodeFiction.InfinityFiction.Core.ResourceBuilder.ItmResourceBundle;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;

namespace InfinityFiction.UI.Modules.ItmModule
{
    public class Module : IInfinityModule
    {
        public void OnRegisterDependencies(IWindsorContainer container)
        {
            container.Register(Component.For<IItmResourceBuilder>().ImplementedBy<ItmResourceBuilder>());
        }
    }
}
