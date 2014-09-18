using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StructConverters.Tests.TestClassess
{
    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Ansi)]
    [Serializable] 
    public struct TestStruct
    {
        [FieldOffset(0x0000)]
        public uint NumberOfStates;

        [FieldOffset(0x0004)]
        public uint StatesOffset;
    }
}
