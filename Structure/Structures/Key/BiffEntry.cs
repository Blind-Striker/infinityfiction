using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Key
{
    [StructLayout(LayoutKind.Explicit, Size = 12, CharSet = CharSet.Ansi)]
    public struct BiffEntry
    {
        [FieldOffset(0x0000)]
        public uint BiffFileLength;

        [FieldOffset(0x0004)]
        public uint BIFFileNameStartOffset;

        [FieldOffset(0x0008)]
        public ushort BIFFileNameLength;

        /// <summary>
        /// The 16 bits of this field are used individually to mark the location of the relevant file.
        /// (MSB) xxxx xxxx ABCD EFGH (LSB)
        /// Bits marked A to F determine on which CD the file is stored (A = CD6, F = CD1)
        /// Bit G determines if the file is in the \cache directory
        /// Bit H determines if the file is in the \data directory
        /// </summary>
        [FieldOffset(0x000a)]
        public ushort FileLocationMark;
    }
}
