using System.IO;
using CodeFiction.InfinityFiction.Core.Container;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Core.StructContainer;

namespace ResourceBuilderSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = Bootstrapper.Create()
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller());

            string chittinKeyPath = Path.Combine(@"G:\Games\BGOrg\BGII - SoA", "CHITIN.KEY");
            string dialogPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\data\lang\en_US", "dialog.tlk");

            //if (System.IO.File.Exists(dialogPath))
            //{
            //    // WshShellClass shell = new WshShellClass();
            //    WshShell shell = new WshShell(); //Create a new WshShell Interface
            //    IWshShortcut link = (IWshShortcut)shell.CreateShortcut(dialogPath); //Link the interface to our shortcut

            //    dialogPath = link.TargetPath;
            //}

            IKeyResourceBuilder keyResourceBuilder = bootstrapper.WindsorContainer.Resolve<IKeyResourceBuilder>();
            IDlgResourceBuilder dlgResourceBuilder = bootstrapper.WindsorContainer.Resolve<IDlgResourceBuilder>();
            
            //KeyResource keyResource = keyResourceBuilder.GetKeyResource(GameEnum.BaldursGateTob, chittinKeyPath);
            DlgResource dlgResource = dlgResourceBuilder.GetDlgResource(dialogPath);
        }
    }
}
