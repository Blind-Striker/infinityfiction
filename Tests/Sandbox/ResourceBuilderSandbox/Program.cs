using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFiction.InfinityFiction.Core.ResourceBuilder;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Structure.StructConverters;

namespace ResourceBuilderSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyResourceBuilder keyResourceBuilder = new KeyResourceBuilder(new GenericStructConverter());

            KeyResource keyResource = keyResourceBuilder.GetKeyResource();
        }
    }
}
