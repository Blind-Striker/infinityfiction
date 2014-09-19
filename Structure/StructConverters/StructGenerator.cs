using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CodeFiction.InfinityFiction.Structure.StructConverterContracts;

using Microsoft.CSharp;

namespace CodeFiction.InfinityFiction.Structure.StructConverters
{
    public class StructGenerator : IStructGenerator
    {
        public Assembly GenerateStruct(string structText)
        {
            CodeDomProvider cpd = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("CodeFiction.InfinityFiction.Structure.Structures.dll");
            cp.GenerateExecutable = false;

            // Invoke compilation.
            CompilerResults cr = cpd.CompileAssemblyFromSource(cp, structText);
            return cr.CompiledAssembly;
        }
    }
}
