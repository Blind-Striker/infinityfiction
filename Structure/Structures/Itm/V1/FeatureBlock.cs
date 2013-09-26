using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Itm.V1
{
    [StructLayout(LayoutKind.Explicit, Size = 48, CharSet = CharSet.Ansi)]
    public struct FeatureBlock
    {
        [FieldOffset(0x0000)]
        public ushort OpcodeNumber;

        [FieldOffset(0x0002)]
        public char TargetType;

        [FieldOffset(0x0003)]
        public char Power;

        [FieldOffset(0x0004)]
        public uint Parameter1;

        [FieldOffset(0x0008)]
        public uint Parameter2;

        [FieldOffset(0x000c)]
        public char TimingMode;

        [FieldOffset(0x000d)]
        public char Resistance;

        [FieldOffset(0x000e)]
        public uint Duration;

        [FieldOffset(0x0012)]
        public char Probability1;

        [FieldOffset(0x0013)]
        public char Probability2;

        [FieldOffset(0x0014)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Resource;

        [FieldOffset(0x001c)]
        public uint DiceThrown;

        [FieldOffset(0x0020)]
        public uint DiceSides;

        [FieldOffset(0x0024)]
        public uint SavingThrowType;

        [FieldOffset(0x0028)]
        public uint SavingThrowBonus;

        [FieldOffset(0x002c)]
        public uint Unknown;
    }
}
