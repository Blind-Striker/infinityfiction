using Castle.MicroKernel.Registration;
using Castle.Windsor;

using CodeFiction.InfinityFiction.Core.BootstrapperLib;
using CodeFiction.InfinityFiction.Core.ResourceBuilder.ItmResourceBundle;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;

using InfinityFiction.UI.Modules.ItmModule.Foundation;
using InfinityFiction.UI.Modules.ItmModule.Presenters;
using InfinityFiction.UI.Modules.Module.Core;

namespace InfinityFiction.UI.Modules.ItmModule
{
    public class ItemModule : IInfinityModule
    {
        public void OnRegisterDependencies(IWindsorContainer container)
        {
            container.Register(
                Component.For<IItmResourceBuilder>().ImplementedBy<ItmResourceBuilder>(),
                Component.For<IItemPresenter>().ImplementedBy<ItemPresenter>().Named("ITM"),
                Component.For<IItemView>().ImplementedBy<ItemUserControl>());
        }
    }
}
