using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Structures.Itm.V1
{
    [StructLayout(LayoutKind.Explicit, Size = 114, CharSet = CharSet.Ansi)]
    public struct Header
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Signature;

        [FieldOffset(0x0004)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string Version;

        [FieldOffset(0x0008)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string UnidentifiedName;

        [FieldOffset(0x000c)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string IdentifiedName;

        [FieldOffset(0x0010)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string ReplacementItem;

        [FieldOffset(0x0018)]
        public uint Flags;

        [FieldOffset(0x001c)]
        public ushort ItemType;

        [FieldOffset(0x001e)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Usability;

        [FieldOffset(0x0022)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public string ItemAnimation;

        [FieldOffset(0x0024)]
        public ushort MinLevel;

        [FieldOffset(0x0026)]
        public ushort MinimumStrength;

        [FieldOffset(0x0028)]
        public char MinStrengthBonus;

        [FieldOffset(0x0029)]
        public char KitUsability1;

        [FieldOffset(0x002a)]
        public char MinIntelligence;

        [FieldOffset(0x002b)]
        public char KitUsability2;

        [FieldOffset(0x002c)]
        public char MinDexterity;

        [FieldOffset(0x002d)]
        public char KitUsability3;

        [FieldOffset(0x002e)]
        public char MinWisdom;

        [FieldOffset(0x002f)]
        public char KitUsability4;

        [FieldOffset(0x0030)]
        public char MinConstitution;

        [FieldOffset(0x0031)]
        public char WeaponProficiency;

        [FieldOffset(0x0032)]
        public ushort MinCharisma;

        [FieldOffset(0x0034)]
        public uint Price;

        [FieldOffset(0x0038)]
        public ushort StackAmount;

        [FieldOffset(0x003a)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string InventoryIcon;

        [FieldOffset(0x0042)]
        public ushort LoreToId;

        [FieldOffset(0x0044)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string GroundIcon;

        [FieldOffset(0x004c)]
        public uint Weight;

        [FieldOffset(0x0050)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string UnidentifiedDescription;

        [FieldOffset(0x0054)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string IdentifiedDescription;

        [FieldOffset(0x0058)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string DescriptionIcon;

        [FieldOffset(0x0060)]
        public uint Enchantment;

        [FieldOffset(0x0064)]
        public uint ExtendedHeadersOffset;

        [FieldOffset(0x0068)]
        public ushort ExtendedHeadersCount;

        [FieldOffset(0x006a)]
        public uint FeatureBlocksOffset;

        [FieldOffset(0x006e)]
        public ushort FirstFeatureBlockIndex;

        [FieldOffset(0x0070)]
        public ushort FeatureBlocksCount;
    }
}
