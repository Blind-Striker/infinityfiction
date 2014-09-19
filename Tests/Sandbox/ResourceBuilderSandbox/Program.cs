using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using CodeFiction.InfinityFiction.Core.BootstrapperLib;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContracts;
using CodeFiction.InfinityFiction.Core.StructContainer;

namespace ResourceBuilderSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = Bootstrapper.Create()
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller())
                .RegisterInstaller(new ServiceInstaller());

            string chittinKeyPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\00766", "CHITIN.KEY");
            string dialogPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\data\lang\en_US", "dialog.tlk");

            var resourceFileProvider = bootstrapper.WindsorContainer.Resolve<IResourceFileProvider>();
            byte[] contentOfFile = resourceFileProvider.GetByteContentOfFile(chittinKeyPath);

            IKeyResourceBuilder keyResourceBuilder = bootstrapper.WindsorContainer.Resolve<IKeyResourceBuilder>();
            //IDlgResourceBuilder dlgResourceBuilder = bootstrapper.WindsorContainer.Resolve<IDlgResourceBuilder>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            KeyResource keyResource = keyResourceBuilder.BuildKeyResource(contentOfFile);
            stopwatch.Stop();
            Console.WriteLine("Miliseconds : {0} - Ticks {1}", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);

            stopwatch.Reset();

            stopwatch.Start();
            KeyResource buildKeyResourceNew = keyResourceBuilder.BuildKeyResourceNew(contentOfFile);
            stopwatch.Stop();
            Console.WriteLine("Miliseconds : {0} - Ticks {1}", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);

            Console.ReadLine();
        }
    }
}
