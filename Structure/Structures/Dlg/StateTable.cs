using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeFiction.InfinityFiction.Structure.Structures.Dlg
{
    [StructLayout(LayoutKind.Explicit, Size = 16, CharSet = CharSet.Ansi)]
    public struct StateTable
    {
        [FieldOffset(0), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string ActorResponseText;

        [FieldOffset(0x0004)]
        public uint FirstResponseIndex;

        [FieldOffset(0x0008)]
        public uint NumberOfResponses;

        [FieldOffset(0x000c)]
        public uint TriggerIndex;
    }
}
