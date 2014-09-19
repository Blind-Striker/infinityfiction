using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Key
{
    [StructLayout(LayoutKind.Explicit)]
    public struct KeyStruct
    {
        [FieldOffset(0)] 
        public Header Header;

        [FieldOffset(0), MarshalAs(UnmanagedType.ByValArray, SizeConst = 0)]
        public BiffEntry[] BiffEntries;

        [FieldOffset(0), MarshalAs(UnmanagedType.ByValArray, SizeConst = 0)]
        public ResourceEntry[] ResourceEntries;
    }
}
