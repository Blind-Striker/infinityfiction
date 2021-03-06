﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using CodeFiction.InfinityFiction.Core.BootstrapperLib;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContainer;
using CodeFiction.InfinityFiction.Core.ServiceContracts;
using CodeFiction.InfinityFiction.Core.StructContainer;

namespace ServicesSandbox
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrapper bootstrapper = Bootstrapper.Create()
                .RegisterInstaller(new ResourceBuilderInstaller())
                .RegisterInstaller(new StructInstaller())
                .RegisterInstaller(new ServiceInstaller());

            var infinityFictionConfigService = bootstrapper.WindsorContainer.Resolve<IInfinityFictionConfigService>();

            string chittinKeyPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\data", "chitin.key");
            //string chittinKeyPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\00766", "chitin.key");

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 7; i++)
            {
                stopwatch.Start();
                infinityFictionConfigService.InitializeConfiguration(chittinKeyPath);
                stopwatch.Stop();
                TimeSpan timeSpan = stopwatch.Elapsed;
                Console.WriteLine("{0} Seconds, {1} Miliseconds, {2} Tics", timeSpan.Seconds, timeSpan.Milliseconds, timeSpan.Ticks);
                stopwatch.Reset();
            }

            Console.ReadLine();
        }
    }
}
