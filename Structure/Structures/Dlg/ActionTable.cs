using System.Runtime.InteropServices;

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
