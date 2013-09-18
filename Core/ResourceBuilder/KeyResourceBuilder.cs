using System;
using System.Collections.Generic;
using System.Linq;
using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class KeyResourceBuilder : IKeyResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;
        private readonly IResourceConverter _resourceConverter;
        private Dictionary<int, string> _extensionMap;

        public KeyResourceBuilder(IGenericStructConverter genericStructConverter, IResourceConverter resourceConverter)
        {
            _genericStructConverter = genericStructConverter;
            _resourceConverter = resourceConverter;
        }

        public KeyResource GetKeyResource(GameEnum gameEnum, string keyFilePath)
        {
            var keyResource = new KeyResource();
            MapExtensions(keyResource, gameEnum);
            _extensionMap = keyResource.ExtensionMap;

            byte[] content = IoHelper.ReadBinaryFile(keyFilePath);

            var header = _genericStructConverter.ConvertToStruct<Header>(content, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;

            keyResource.Header = new HeaderResource();
            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesCount];

            uint sizeOfBiffEntry;
            const uint SizeOfResourceEntry = 14;
            unsafe
            {
                sizeOfBiffEntry = (uint)sizeof(BiffEntry);
            }

            _resourceConverter.Convert(header, keyResource.Header);
            _resourceConverter.Convert<BiffEntry, BiffEntryResource>(content, keyResource.BiffEntries, biffEntriesOffset, sizeOfBiffEntry);
            _resourceConverter.Convert<ResourceEntry, ResourceEntryResource>(content, keyResource.ResourceEntries, resourceEntriesOffset, SizeOfResourceEntry, OnResourceConverted);

            return keyResource;
        }

        public void MapExtensions(KeyResource keyResource, GameEnum gameEnum)
        {
            keyResource.ExtensionMap = new Dictionary<int, string>();

            if (gameEnum == GameEnum.NewerwinterNights)
            {
                keyResource.ExtensionMap.Add(0x001, "BMP");
                keyResource.ExtensionMap.Add(0x003, "TGA");
                keyResource.ExtensionMap.Add(0x004, "WAV");
                keyResource.ExtensionMap.Add(0x006, "PLT");
                keyResource.ExtensionMap.Add(0x007, "INI");
                keyResource.ExtensionMap.Add(0x00A, "TXT");
                keyResource.ExtensionMap.Add(0x7D2, "MDL"); // Aurora model - not supported
                keyResource.ExtensionMap.Add(0x7D9, "NSS");
                keyResource.ExtensionMap.Add(0x7DA, "NCS");
                keyResource.ExtensionMap.Add(0x7DC, "ARE");
                keyResource.ExtensionMap.Add(0x7DD, "SET");
                keyResource.ExtensionMap.Add(0x7DE, "IFO");
                keyResource.ExtensionMap.Add(0x7DF, "BIC");
                keyResource.ExtensionMap.Add(0x7E0, "WOK");
                keyResource.ExtensionMap.Add(0x7E1, "2DA");
                keyResource.ExtensionMap.Add(0x7E6, "TXI");
                keyResource.ExtensionMap.Add(0x7E7, "GIT");
                keyResource.ExtensionMap.Add(0x7E9, "UTI");
                keyResource.ExtensionMap.Add(0x7EB, "UTC");
                keyResource.ExtensionMap.Add(0x7ED, "DLG");
                keyResource.ExtensionMap.Add(0x7EE, "ITP");
                keyResource.ExtensionMap.Add(0x7F0, "UTT");
                keyResource.ExtensionMap.Add(0x7F1, "DDS"); // Compressed texture file - not supported
                keyResource.ExtensionMap.Add(0x7F3, "UTS");
                keyResource.ExtensionMap.Add(0x7F4, "LTR"); // Letter-combo probability info for name generation - not supported
                keyResource.ExtensionMap.Add(0x7F5, "GFF");
                keyResource.ExtensionMap.Add(0x7F6, "FAC");
                keyResource.ExtensionMap.Add(0x7F8, "UTE");
                keyResource.ExtensionMap.Add(0x7FA, "UTD");
                keyResource.ExtensionMap.Add(0x7FC, "UTP");
                keyResource.ExtensionMap.Add(0x7FD, "DFT");
                keyResource.ExtensionMap.Add(0x7FE, "GIC");
                keyResource.ExtensionMap.Add(0x7FF, "GUI");
                keyResource.ExtensionMap.Add(0x803, "UTM");
                keyResource.ExtensionMap.Add(0x804, "DWK");
                keyResource.ExtensionMap.Add(0x805, "PWK");
                keyResource.ExtensionMap.Add(0x808, "JRL");
                keyResource.ExtensionMap.Add(0x80A, "UTW");
                keyResource.ExtensionMap.Add(0x80C, "SSF");
                keyResource.ExtensionMap.Add(0x810, "NDB");
                keyResource.ExtensionMap.Add(0x811, "PTM");
                keyResource.ExtensionMap.Add(0x812, "PTT");
            }
            else if (gameEnum == GameEnum.Kotor || gameEnum == GameEnum.Kotor2)
            {
                keyResource.ExtensionMap.Add(0x000, "INV");
                keyResource.ExtensionMap.Add(0x003, "TGA");
                keyResource.ExtensionMap.Add(0x004, "WAV");
                keyResource.ExtensionMap.Add(0x7D2, "MDL"); // Aurora model - not supported
                keyResource.ExtensionMap.Add(0x7D9, "NSS");
                keyResource.ExtensionMap.Add(0x7DA, "NCS");
                keyResource.ExtensionMap.Add(0x7DC, "ARE");
                keyResource.ExtensionMap.Add(0x7DE, "IFO");
                keyResource.ExtensionMap.Add(0x7DF, "BIC");
                keyResource.ExtensionMap.Add(0x7E0, "BWM"); // ?????
                keyResource.ExtensionMap.Add(0x7E1, "2DA");
                keyResource.ExtensionMap.Add(0x7E6, "TXI");
                keyResource.ExtensionMap.Add(0x7E7, "GIT");
                keyResource.ExtensionMap.Add(0x7E8, "BTI");
                keyResource.ExtensionMap.Add(0x7E9, "UTI");
                keyResource.ExtensionMap.Add(0x7EA, "BTC");
                keyResource.ExtensionMap.Add(0x7EB, "UTC");
                keyResource.ExtensionMap.Add(0x7ED, "DLG");
                keyResource.ExtensionMap.Add(0x7EE, "ITP");
                keyResource.ExtensionMap.Add(0x7F0, "UTT");
                keyResource.ExtensionMap.Add(0x7F3, "UTS");
                keyResource.ExtensionMap.Add(0x7F4, "LTR"); // Letter-combo probability info for name generation - not supported
                keyResource.ExtensionMap.Add(0x7F6, "FAC");
                keyResource.ExtensionMap.Add(0x7F8, "UTE");
                keyResource.ExtensionMap.Add(0x7FA, "UTD");
                keyResource.ExtensionMap.Add(0x7FC, "UTP");
                keyResource.ExtensionMap.Add(0x7FF, "GUI");
                keyResource.ExtensionMap.Add(0x803, "UTM");
                keyResource.ExtensionMap.Add(0x804, "BWM"); // ??????
                keyResource.ExtensionMap.Add(0x805, "BWM"); // ??????
                keyResource.ExtensionMap.Add(0x808, "JRL");
                keyResource.ExtensionMap.Add(0x809, "MOD"); // MOD 1.0 - name might be incorrect
                keyResource.ExtensionMap.Add(0x80A, "UTW");
                keyResource.ExtensionMap.Add(0x80C, "SSF");
                keyResource.ExtensionMap.Add(0xBBB, "PTH");
                keyResource.ExtensionMap.Add(0xBBC, "LIP"); // ??? binary format
            }
            else
            {
                keyResource.ExtensionMap.Add(0x001, "BMP");
                keyResource.ExtensionMap.Add(0x002, "MVE");
                keyResource.ExtensionMap.Add(0x004, "WAV");
                keyResource.ExtensionMap.Add(0x005, "WFX");
                keyResource.ExtensionMap.Add(0x006, "PLT");
                keyResource.ExtensionMap.Add(0x3e8, "BAM");
                keyResource.ExtensionMap.Add(0x3e9, "WED");
                keyResource.ExtensionMap.Add(0x3ea, "CHU");
                keyResource.ExtensionMap.Add(0x3eb, "TIS");
                keyResource.ExtensionMap.Add(0x3ec, "MOS");
                keyResource.ExtensionMap.Add(0x3ed, "ITM");
                keyResource.ExtensionMap.Add(0x3ee, "SPL");
                keyResource.ExtensionMap.Add(0x3ef, "BCS");
                keyResource.ExtensionMap.Add(0x3f0, "IDS");
                keyResource.ExtensionMap.Add(0x3f1, "CRE");
                keyResource.ExtensionMap.Add(0x3f2, "ARE");
                keyResource.ExtensionMap.Add(0x3f3, "DLG");
                keyResource.ExtensionMap.Add(0x3f4, "2DA");
                keyResource.ExtensionMap.Add(0x3f5, "GAM");
                keyResource.ExtensionMap.Add(0x3f6, "STO");
                keyResource.ExtensionMap.Add(0x3f7, "WMP");
                keyResource.ExtensionMap.Add(0x3f8, "EFF");
                keyResource.ExtensionMap.Add(0x3f9, "BS");
                keyResource.ExtensionMap.Add(0x3fa, "CHR");
                keyResource.ExtensionMap.Add(0x3fb, "VVC");
                keyResource.ExtensionMap.Add(0x3fc, "VEF"); // ????????
                keyResource.ExtensionMap.Add(0x3fd, "PRO");
                keyResource.ExtensionMap.Add(0x3fe, "BIO");
                keyResource.ExtensionMap.Add(0x404, "PVR");
                keyResource.ExtensionMap.Add(0x44c, "BAH"); // ???????
                keyResource.ExtensionMap.Add(0x802, "INI");
                keyResource.ExtensionMap.Add(0x803, "SRC");
            }
        }

        private void OnResourceConverted(ResourceEntryResource resourceEntryResource)
        {
            string fileName = resourceEntryResource.Properties.Where(type => type.Name == "ResourceName").Select(type => type.Value.ToString().Trim()).First();
            int resourceType = resourceEntryResource.Properties.Where(type => type.Name == "ResourceType").Select(type => Convert.ToInt32(type.Value)).First();
            if (_extensionMap.ContainsKey(resourceType))
            {
                resourceEntryResource.Extension = _extensionMap[resourceType];
                resourceEntryResource.FileName = string.Format("{0}.{1}", fileName, resourceEntryResource.Extension);
            }
        }
    }
}
