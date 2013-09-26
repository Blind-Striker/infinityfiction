using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeFiction.InfinityFiction.Structure.Structures.Itm.V1
{
    [StructLayout(LayoutKind.Explicit, Size = 56, CharSet = CharSet.Ansi)]
    public struct ExtendedHeader
    {
        [FieldOffset(0x0000)]
        public char AttackType;

        [FieldOffset(0x0001)]
        public char IdReq;

        [FieldOffset(0x0002)]
        public char Location;

        [FieldOffset(0x0003)]
        public char AlternativeDiceSides;

        [FieldOffset(0x0004)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string UseIcon;

        [FieldOffset(0x000c)]
        public char TargetType;

        [FieldOffset(0x000d)]
        public char TargetCount;

        [FieldOffset(0x000e)]
        public ushort Range;

        [FieldOffset(0x0010)]
        public byte ProjectileType;

        [FieldOffset(0x0011)]
        public byte AlternativeDiceThrown;

        [FieldOffset(0x0012)]
        public byte Speed;

        [FieldOffset(0x0013)]
        public byte AlternativeDamageBonus;

        [FieldOffset(0x0014)]
        public ushort Thac0Bonus;

        [FieldOffset(0x0016)]
        public byte DiceSides;

        [FieldOffset(0x0017)]
        public byte PrimaryType;

        [FieldOffset(0x0018)]
        public byte DiceThrown;

        [FieldOffset(0x0019)]
        public byte SecondaryType;

        [FieldOffset(0x001a)]
        public ushort DamageBonus;

        [FieldOffset(0x001c)]
        public ushort DamageType;

        [FieldOffset(0x001e)]
        public ushort FeatureBlocksCount;

        [FieldOffset(0x0020)]
        public ushort FeatureBlocksOffset;

        [FieldOffset(0x0022)]
        public ushort Charges;

        [FieldOffset(0x0024)]
        public ushort ChargeDepletionBehaviour;

        [FieldOffset(0x0026)]
        public uint Flags;

        [FieldOffset(0x002a)]
        public ushort ProjectileAnimation;

        [FieldOffset(0x002c)]
        public ushort OverhandSwingAnimation;

        [FieldOffset(0x002e)]
        public ushort BackhandSwingAnimation;

        [FieldOffset(0x0030)]
        public ushort ThrustAnimation;

        [FieldOffset(0x0032)]
        public ushort IsArrow;

        [FieldOffset(0x0034)]
        public ushort IsBolt;

        [FieldOffset(0x0036)]
        public ushort IsBullet;
    }
}
