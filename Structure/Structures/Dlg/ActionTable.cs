using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeFiction.InfinityFiction.Structure.Structures.Dlg
{
    [StructLayout(LayoutKind.Explicit, Size = 8, CharSet = CharSet.Ansi)]
    public struct ActionTable
    {
        [FieldOffset(0x0000)]
        public uint ActionStringOffset;

        [FieldOffset(0x0004)]
        public uint ActionStringLength;
    }
}
