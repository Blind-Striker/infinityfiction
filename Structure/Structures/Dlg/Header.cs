using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Dlg
{
    [StructLayout(LayoutKind.Explicit, Size = 52, CharSet = CharSet.Ansi)]
    public struct Header
    {
        [FieldOffset(0), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Signature;

        [FieldOffset(0x0004), MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Version;

        [FieldOffset(0x0008)] 
        public uint NumberOfStates;

        [FieldOffset(0x000c)]
        public uint StatesOffset;

        [FieldOffset(0x0010)]
        public uint NumberOfTransitions;

        [FieldOffset(0x0014)]
        public uint TransitionsOffset;

        [FieldOffset(0x0018)]
        public uint StateTriggerOffset;

        [FieldOffset(0x001c)]
        public uint NumberStateTriggers;

        [FieldOffset(0x0020)]
        public uint TransitionTriggerOffset;

        [FieldOffset(0x0024)]
        public uint NumberTransitionTriggers;

        [FieldOffset(0x0028)]
        public uint ActionTableOffset;

        [FieldOffset(0x002c)]
        public uint NumberActions;

        [FieldOffset(0x0030)]
        public uint ThreatResponseFlags;
    }
}
