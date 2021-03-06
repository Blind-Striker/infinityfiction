﻿using System;
using System.Windows.Forms;

using CodeFiction.InfinityFiction.Core.BootstrapperLib;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContainer;
using CodeFiction.InfinityFiction.Core.StructContainer;
using InfinityFiction.UI.InfinityFictionEditor.Core;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.Modules.ItmModule;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Bootstrapper registerInstaller = Bootstrapper.Create()
                .RegisterInstaller(new ServiceInstaller())
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller())
                .RegisterInstaller(new InfinityFictionEditorCoreInstaller())
                .RegisterInstaller(new InfinityFictionEditorInstaller())

                .RegisterModule<ItemModule>()
                
                .InitializeApplication();

            var presenterFactory = registerInstaller.WindsorContainer.Resolve<IPresenterFactory>();
            var mainPresenter = presenterFactory.CreatePresenter<IMainPresenter>();

            Application.Run(mainPresenter.View as Form);
        }
    }
}
