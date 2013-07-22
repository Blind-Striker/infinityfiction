using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Key
{
    [StructLayout(LayoutKind.Explicit, Size = 24, CharSet = CharSet.Ansi)]
    public struct Header
    {
        [FieldOffset(0), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Signature;

        [FieldOffset(0x0004), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Version;

        [FieldOffset(0x0008)] 
        public uint BiffEntriesCount;

        [FieldOffset(0x000c)] 
        public uint ResourceEntriesCount;

        [FieldOffset(0x0010)] 
        public uint BiffEntriesOffset;

        [FieldOffset(0x0014)] 
        public uint ResourceEntriesOffset;
    }
}
