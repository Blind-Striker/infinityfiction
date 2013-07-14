using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeFiction.InfinityFiction.Structure.Structures.Key
{
    [StructLayout(LayoutKind.Explicit, Size = 14, CharSet = CharSet.Ansi)]
    public struct ResourceEntry
    {
        [FieldOffset(0), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ResourceName;
        
        [FieldOffset(0x0008)]
        public ushort ResourceType;

        [FieldOffset(0x000a)] 
        public uint ResourceLocator;
    }
}
