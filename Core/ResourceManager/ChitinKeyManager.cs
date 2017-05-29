using System;
using System.Runtime.InteropServices;

using CodeFiction.InfinityFiction.Structure.Resources.Models;
using CodeFiction.InfinityFiction.Structure.Resources.Structures;

namespace CodeFiction.InfinityFiction.Core.ResourceManager
{
    public class ChitinKeyManager : IDisposable
    {
        const string DllLocation = "InfinityEngineNativeResourceLibrary.dll";

        [DllImport(DllLocation, EntryPoint = "ReadKeyFile", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool ReadKeyFile(string filePath, ref KeyStruct pKeyFile);

        public ChitinKeyResource GetChitinKey(string filePath)
        {
            KeyStruct keyStruct = new KeyStruct();
            bool readKeyFile = ReadKeyFile(filePath, ref keyStruct);

            if (!readKeyFile)
            {
                return null;
            }

            ChitinKeyResource chitinKeyResource = new ChitinKeyResource();

            chitinKeyResource.Header = keyStruct.Header;
            chitinKeyResource.BiffEntries = GetStructArrays<BiffEntryStruct>(keyStruct.Header.BiffEntriesCount, keyStruct.BiffEntries);
            chitinKeyResource.ResourceEntries = GetStructArrays<ResourceEntryStruct>(keyStruct.Header.ResourceEntriesCount, keyStruct.ResourceEntries);
            chitinKeyResource.BiffInfos = GetStructArrays<BiffInfoStruct>(keyStruct.Header.BiffEntriesCount, keyStruct.BiffInfos);

            return chitinKeyResource;
        }

        private TStruct[] GetStructArrays<TStruct>(uint count, IntPtr pointer)
        {
            TStruct[] structs = new TStruct[count];
            int sizeOf = Marshal.SizeOf<TStruct>();

            for (int i = 0; i < count; i++)
            {
                TStruct @struct = Marshal.PtrToStructure<TStruct>(pointer);
                structs[i] = @struct;
                pointer = IntPtr.Add(pointer, sizeOf);
            }

            return structs;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        ~ChitinKeyManager()
        {
            ReleaseUnmanagedResources();
        }
    }
}
