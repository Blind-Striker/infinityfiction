using System;
using System.Runtime.InteropServices;

namespace CodeFiction.InfinityFiction.Structure.Resources.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Signature;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string Version;

        public uint BiffEntriesCount;

        public uint ResourceEntriesCount;

        public uint BiffEntriesOffset;

        public uint ResourceEntriesOffset;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BiffEntryStruct
    {
        public uint BiffFileLength;

        public uint BiffFileNameStartOffset;

        public ushort BiffFileNameLength;

        public ushort FileLocationMark;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BiffInfoStruct
    {
        public IntPtr FileName;

        public ushort Location;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ResourceEntryStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string ResourceName;

        public ushort ResourceType;

        public uint ResourceLocator;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct KeyStruct
    {
        public Header Header;

        public IntPtr BiffEntries;

        public IntPtr ResourceEntries;

        public IntPtr BiffInfos;
    }
}
