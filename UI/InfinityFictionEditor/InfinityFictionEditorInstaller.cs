using System.Configuration;
using System.Waf.Applications.Services;
using System.Waf.Presentation.WinForms.Services;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Properties;

using MvpVmFramework.Core.Foundation;
using MvpVmFramework.Core.Services;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    public class InfinityFictionEditorInstaller : IWindsorInstaller 
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMainView>().ImplementedBy<MainForm>().LifestyleTransient(),
                Component.For<ISelectGamePathView>().ImplementedBy<SelectGamePathForm>().LifestyleTransient(),

                Component.For<IMessageService>().ImplementedBy<MessageService>().LifestyleTransient(),
                Component.For<IDialogService>().ImplementedBy<WindowsFormDialogService>().LifestyleTransient());
        }
    }
}
