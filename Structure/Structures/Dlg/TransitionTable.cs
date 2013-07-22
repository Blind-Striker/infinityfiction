using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Dlg
{
    [StructLayout(LayoutKind.Explicit, Size = 32, CharSet = CharSet.Ansi)]
    public struct TransitionTable
    {
        [FieldOffset(0x0000)]
        public uint Flag;

        [FieldOffset(0x0004)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string AssociatedText;

        [FieldOffset(0x0008)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string JournalEntry;

        [FieldOffset(0x000c)]
        public uint TriggerIndex;

        [FieldOffset(0x0010)]
        public uint ActionIndex;

        [FieldOffset(0x0014)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string NextDialogue;

        [FieldOffset(0x001c)]
        public uint NextDialogueState;
    }
}
