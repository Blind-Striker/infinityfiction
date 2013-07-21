using System;
using System.Windows.Forms;
using CodeFiction.InfinityFiction.Core.Container;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContainer;
using CodeFiction.InfinityFiction.Core.StructContainer;

using InfinityFiction.UI.InfinityFictionEditor.Core;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Bootstrapper registerInstaller = Bootstrapper.Create()
                .RegisterInstaller(new ServiceInstaller())
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller())
                .RegisterInstaller(new InfinityFictionEditorCoreInstaller())
                .RegisterInstaller(new InfinityFictionEditorInstaller());

            IPresenterFactory presenterFactory = registerInstaller.WindsorContainer.Resolve<IPresenterFactory>();
            IMainPresenter mainPresenter = presenterFactory.CreatePresenter<IMainPresenter>();

            Application.Run(mainPresenter.View as Form);
        }
    }
}
