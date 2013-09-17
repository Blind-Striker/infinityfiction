using System;
using System.Diagnostics;
using System.IO;
using CodeFiction.InfinityFiction.Core.Container;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContracts;
using CodeFiction.InfinityFiction.Core.StructContainer;

namespace ServicesSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = Bootstrapper.Create()
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller())
                .RegisterInstaller(new ServiceInstaller());

            var infinityFictionConfigService = bootstrapper.WindsorContainer.Resolve<IInfinityFictionConfigService>();

            string chittinKeyPath = Path.Combine(@"G:\Games\BGOrg\BGII - SoA", "CHITIN.KEY");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            infinityFictionConfigService.InitializeConfiguration(chittinKeyPath);
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine(timeSpan.ToString());
            Console.ReadLine();
        }
    }
}
