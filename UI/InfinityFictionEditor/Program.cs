using System;
using System.Windows.Forms;
using CodeFiction.InfinityFiction.Core.Container;
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
                .RegisterInstaller(new InfinityFictionEditorCoreInstaller())
                .RegisterInstaller(new InfinityFictionEditorInstaller());

            IPresenterFactory presenterFactory = registerInstaller.WindsorContainer.Resolve<IPresenterFactory>();
            IMainPresenter mainPresenter = presenterFactory.CreatePresenter<IMainPresenter>();

            Application.Run(mainPresenter.View as Form);
        }
    }
}
