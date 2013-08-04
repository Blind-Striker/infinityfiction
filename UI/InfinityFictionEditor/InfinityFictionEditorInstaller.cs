using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    public class InfinityFictionEditorInstaller : IWindsorInstaller 
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMainView>().ImplementedBy<MainForm>().LifestyleTransient(),
                Component.For<ISelectGamePathView>().ImplementedBy<SelectGamePathForm>().LifestyleTransient());
        }
    }
}
