using System.Waf.Presentation.WinForms;

using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.Presenters;
using InfinityFiction.UI.InfinityFictionEditor.Core.Settings;
using InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls.CommandFactories;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core
{
    public class InfinityFictionEditorCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            CommandAdapter.AddCommandBindingFactory(new TreeViewCommandBindingFactory());

            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<IAppSettings>().ImplementedBy<IsolatedStorageAppSettings>().LifestyleSingleton(),

                Component.For<IPresenterFactory>().AsFactory().LifestyleTransient(),
                Component.For<IMainPresenter>().ImplementedBy<MainPresenter>().LifestyleTransient(),
                Component.For<ISelectGamePathPresenter>().ImplementedBy<SelectGamePathPresenter>().LifestyleTransient());
        }
    }
}
