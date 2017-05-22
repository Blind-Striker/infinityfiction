using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            string chittinKeyPath = Path.Combine(@"D:\Games\Baldur's Gate Enhanced Edition\Data\00766", "CHITIN.KEY");
            string dialogPath = Path.Combine(@"D:\Games\Baldur's Gate Enhanced Edition\Data\data\lang\en_US", "dialog.tlk");

            var resourceFileProvider = bootstrapper.WindsorContainer.Resolve<IResourceFileProvider>();
            byte[] contentOfFile = resourceFileProvider.GetByteContentOfFile(chittinKeyPath);


            IKeyResourceBuilder keyResourceBuilder = bootstrapper.WindsorContainer.Resolve<IKeyResourceBuilder>();
            //IDlgResourceBuilder dlgResourceBuilder = bootstrapper.WindsorContainer.Resolve<IDlgResourceBuilder>();

            IList<long> b1 = new List<long>();
            IList<long> b2 = new List<long>();

            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 20; i++)
            {
                stopwatch.Start();
                KeyResource keyResource = keyResourceBuilder.BuildKeyResource(contentOfFile);
                stopwatch.Stop();
                Console.WriteLine("BuildKeyResource Miliseconds : {0} - Ticks {1}", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);
                b1.Add(stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();

                stopwatch.Start();
                KeyResource buildKeyResourceNew = keyResourceBuilder.BuildKeyResourceNew(contentOfFile);
                stopwatch.Stop();
                Console.WriteLine("BuildKeyResourceNew Miliseconds : {0} - Ticks {1}", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);
                b2.Add(stopwatch.ElapsedMilliseconds);

                stopwatch.Reset();
            }

            Console.WriteLine("BuildKeyResource Avg : {0}", b1.Average());
            Console.WriteLine("BuildKeyResourceNew Avg : {0}", b2.Average());

            Console.ReadLine();
        }

        public static byte[] ReadBinaryFile(string filePath, long? length = null)
        {
            FileStream fileStream = (FileStream)null;
            BinaryReader binaryReader = (BinaryReader)null;
            byte[] numArray;
            try
            {
                length = length ?? new FileInfo(filePath).Length;
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                binaryReader = new BinaryReader((Stream)fileStream);
                numArray = binaryReader.ReadBytes((int)length);
            }
            catch (Exception ex)
            {
                Exception ioLibException = new Exception("Dosya okuma işlemi sırasında hata oluştu", ex);
                ioLibException.Data.Add((object)"Path", (object)filePath);
                throw ioLibException;
            }
            finally
            {
                if (binaryReader != null)
                    binaryReader.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
            return numArray;
        }
    }

}
