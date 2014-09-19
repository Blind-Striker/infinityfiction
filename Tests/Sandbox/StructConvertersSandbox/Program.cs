using System;
using System.CodeDom;
using System.IO;

using CodeFiction.InfinityFiction.Core.BootstrapperLib;
using CodeFiction.InfinityFiction.Core.ResourceContainer;
using CodeFiction.InfinityFiction.Core.Services;
using CodeFiction.InfinityFiction.Core.StructContainer;
using System.Reflection;
using System.CodeDom.Compiler;

using CodeFiction.InfinityFiction.Structure.StructConverters;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

using Microsoft.CSharp;

namespace StructConvertersSandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			Bootstrapper bootstrapper = Bootstrapper.Create()
				.RegisterInstaller(new ResourceBuilderInstaller())
				.RegisterInstaller(new StructInstaller());

            string chittinKeyPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\00766", "CHITIN.KEY");
			string dialogPath = Path.Combine(@"C:\Program Files (x86)\Baldur's Gate Enhanced Edition\Data\data\lang\en_US", "dialog.tlk");

			ResourceFileProvider provider = new ResourceFileProvider();
            GenericStructConverter genericStructConverter = new GenericStructConverter();
		    byte[] byteContentOfFile = provider.GetByteContentOfFile(chittinKeyPath);

            var header = genericStructConverter.ConvertToStruct<Header>(byteContentOfFile, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;
		    int headerSize = 24;

		    const string StructTemplate = @"using System.Runtime.InteropServices;

            namespace CodeFiction.InfinityFiction.Structure.Structures.Key
            {{
	            [StructLayout(LayoutKind.Explicit)]
	            public struct KeyStruct
	            {{
		            [FieldOffset(0)] 
		            public Header Header;

		            [FieldOffset({0}), MarshalAs(UnmanagedType.ByValArray, SizeConst = {2})]
		            public BiffEntry[] BiffEntries;

		            [FieldOffset({1}), MarshalAs(UnmanagedType.ByValArray, SizeConst = {3})]
		            public ResourceEntry[] ResourceEntries;
	            }}
            }}";

		    string source = string.Format(StructTemplate, biffEntriesOffset, resourceEntriesOffset, biffEntriesCount, resourceEntriesCount);

			Assembly compileSource = CompileSource(source);

			Type type = compileSource.GetType("CodeFiction.InfinityFiction.Structure.Structures.Key.KeyStruct");


		    object convertToStruct = genericStructConverter.ConvertToStruct(type, byteContentOfFile, 0);
		}

		private static Assembly CompileSource(string sourceCode)
		{
			CodeDomProvider cpd = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();
			cp.ReferencedAssemblies.Add("System.dll");
			cp.ReferencedAssemblies.Add("CodeFiction.InfinityFiction.Structure.Structures.dll");
			cp.GenerateExecutable = false;

			// Invoke compilation.
			CompilerResults cr = cpd.CompileAssemblyFromSource(cp, sourceCode);
			return cr.CompiledAssembly;
		}
	}
}
