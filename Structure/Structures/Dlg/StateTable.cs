using System.Runtime.InteropServices;

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
