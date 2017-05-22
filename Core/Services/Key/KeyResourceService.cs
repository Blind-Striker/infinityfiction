using System;
using System.Collections.Generic;

using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContracts;

namespace CodeFiction.InfinityFiction.Core.Services.Key
{
    public class KeyResourceService : IKeyResourceService
    {
        private readonly IResourceFileProvider _fileProvider;
        private readonly IKeyResourceBuilder _keyResourceBuilder;

        private FileExtensionMap _fileExtensionMap;

        public KeyResourceService(IResourceFileProvider fileProvider, IKeyResourceBuilder keyResourceBuilder)
        {
            _fileProvider = fileProvider;
            _keyResourceBuilder = keyResourceBuilder;
        }

        public KeyResource GetKeyResource(GameEnum gameEnum, string keyfilePath)
        {
            byte[] content = _fileProvider.GetByteContentOfFile(keyfilePath);
            _fileExtensionMap = MapExtensions(gameEnum);

            KeyResource keyResource = _keyResourceBuilder.BuildKeyResource(content);

            foreach (var resourceEntryResource in keyResource.ResourceEntries)
            {
                SetResourceEntryFileInformation(resourceEntryResource);
            }

            return keyResource;
        }


        private void SetResourceEntryFileInformation(ResourceEntryResource resourceEntryResource)
        {
            string fileName = resourceEntryResource.Properties["ResourceName"].Value.ToString().Trim();
            int resourceType = Convert.ToInt32(resourceEntryResource.Properties["ResourceType"].Value);

            string extension;
            if (_fileExtensionMap.TryGetValue(resourceType, out extension))
            {
                resourceEntryResource.Extension = extension;
                resourceEntryResource.FileName = string.Format("{0}.{1}", fileName, resourceEntryResource.Extension);
            }
            else
            {
                string unknownMark = string.Format("({0}h)", resourceType.ToString("X3"));
                resourceEntryResource.Extension = string.Format("Unknown {0}", unknownMark);
                resourceEntryResource.FileName = string.Format("{0}.Unknown {1}", fileName, unknownMark);
            }
        }

        private FileExtensionMap MapExtensions(GameEnum gameEnum)
        {
            var extensionMap = new FileExtensionMap();

            if (gameEnum == GameEnum.NewerwinterNights)
            {
                extensionMap.Add(0x001, "BMP");
                extensionMap.Add(0x003, "TGA");
                extensionMap.Add(0x004, "WAV");
                extensionMap.Add(0x006, "PLT");
                extensionMap.Add(0x007, "INI");
                extensionMap.Add(0x00A, "TXT");
                extensionMap.Add(0x7D2, "MDL"); // Aurora model - not supported
                extensionMap.Add(0x7D9, "NSS");
                extensionMap.Add(0x7DA, "NCS");
                extensionMap.Add(0x7DC, "ARE");
                extensionMap.Add(0x7DD, "SET");
                extensionMap.Add(0x7DE, "IFO");
                extensionMap.Add(0x7DF, "BIC");
                extensionMap.Add(0x7E0, "WOK");
                extensionMap.Add(0x7E1, "2DA");
                extensionMap.Add(0x7E6, "TXI");
                extensionMap.Add(0x7E7, "GIT");
                extensionMap.Add(0x7E9, "UTI");
                extensionMap.Add(0x7EB, "UTC");
                extensionMap.Add(0x7ED, "DLG");
                extensionMap.Add(0x7EE, "ITP");
                extensionMap.Add(0x7F0, "UTT");
                extensionMap.Add(0x7F1, "DDS"); // Compressed texture file - not supported
                extensionMap.Add(0x7F3, "UTS");
                extensionMap.Add(0x7F4, "LTR"); // Letter-combo probability info for name generation - not supported
                extensionMap.Add(0x7F5, "GFF");
                extensionMap.Add(0x7F6, "FAC");
                extensionMap.Add(0x7F8, "UTE");
                extensionMap.Add(0x7FA, "UTD");
                extensionMap.Add(0x7FC, "UTP");
                extensionMap.Add(0x7FD, "DFT");
                extensionMap.Add(0x7FE, "GIC");
                extensionMap.Add(0x7FF, "GUI");
                extensionMap.Add(0x803, "UTM");
                extensionMap.Add(0x804, "DWK");
                extensionMap.Add(0x805, "PWK");
                extensionMap.Add(0x808, "JRL");
                extensionMap.Add(0x80A, "UTW");
                extensionMap.Add(0x80C, "SSF");
                extensionMap.Add(0x810, "NDB");
                extensionMap.Add(0x811, "PTM");
                extensionMap.Add(0x812, "PTT");
            }
            else if (gameEnum == GameEnum.Kotor || gameEnum == GameEnum.Kotor2)
            {
                extensionMap.Add(0x000, "INV");
                extensionMap.Add(0x003, "TGA");
                extensionMap.Add(0x004, "WAV");
                extensionMap.Add(0x7D2, "MDL"); // Aurora model - not supported
                extensionMap.Add(0x7D9, "NSS");
                extensionMap.Add(0x7DA, "NCS");
                extensionMap.Add(0x7DC, "ARE");
                extensionMap.Add(0x7DE, "IFO");
                extensionMap.Add(0x7DF, "BIC");
                extensionMap.Add(0x7E0, "BWM"); // ?????
                extensionMap.Add(0x7E1, "2DA");
                extensionMap.Add(0x7E6, "TXI");
                extensionMap.Add(0x7E7, "GIT");
                extensionMap.Add(0x7E8, "BTI");
                extensionMap.Add(0x7E9, "UTI");
                extensionMap.Add(0x7EA, "BTC");
                extensionMap.Add(0x7EB, "UTC");
                extensionMap.Add(0x7ED, "DLG");
                extensionMap.Add(0x7EE, "ITP");
                extensionMap.Add(0x7F0, "UTT");
                extensionMap.Add(0x7F3, "UTS");
                extensionMap.Add(0x7F4, "LTR"); // Letter-combo probability info for name generation - not supported
                extensionMap.Add(0x7F6, "FAC");
                extensionMap.Add(0x7F8, "UTE");
                extensionMap.Add(0x7FA, "UTD");
                extensionMap.Add(0x7FC, "UTP");
                extensionMap.Add(0x7FF, "GUI");
                extensionMap.Add(0x803, "UTM");
                extensionMap.Add(0x804, "BWM"); // ??????
                extensionMap.Add(0x805, "BWM"); // ??????
                extensionMap.Add(0x808, "JRL");
                extensionMap.Add(0x809, "MOD"); // MOD 1.0 - name might be incorrect
                extensionMap.Add(0x80A, "UTW");
                extensionMap.Add(0x80C, "SSF");
                extensionMap.Add(0xBBB, "PTH");
                extensionMap.Add(0xBBC, "LIP"); // ??? binary format
            }
            else
            {
                extensionMap.Add(0x001, "BMP");
                extensionMap.Add(0x002, "MVE");
                extensionMap.Add(0x004, "WAV");
                extensionMap.Add(0x005, "WFX");
                extensionMap.Add(0x006, "PLT");
                extensionMap.Add(0x3e8, "BAM");
                extensionMap.Add(0x3e9, "WED");
                extensionMap.Add(0x3ea, "CHU");
                extensionMap.Add(0x3eb, "TIS");
                extensionMap.Add(0x3ec, "MOS");
                extensionMap.Add(0x3ed, "ITM");
                extensionMap.Add(0x3ee, "SPL");
                extensionMap.Add(0x3ef, "BCS");
                extensionMap.Add(0x3f0, "IDS");
                extensionMap.Add(0x3f1, "CRE");
                extensionMap.Add(0x3f2, "ARE");
                extensionMap.Add(0x3f3, "DLG");
                extensionMap.Add(0x3f4, "2DA");
                extensionMap.Add(0x3f5, "GAM");
                extensionMap.Add(0x3f6, "STO");
                extensionMap.Add(0x3f7, "WMP");
                extensionMap.Add(0x3f8, "EFF");
                extensionMap.Add(0x3f9, "BS");
                extensionMap.Add(0x3fa, "CHR");
                extensionMap.Add(0x3fb, "VVC");
                extensionMap.Add(0x3fc, "VEF"); // ????????
                extensionMap.Add(0x3fd, "PRO");
                extensionMap.Add(0x3fe, "BIO");
                extensionMap.Add(0x400, "FNT");
                extensionMap.Add(0x402, "GUI");
                extensionMap.Add(0x403, "SQL");
                extensionMap.Add(0x404, "PVRZ");
                extensionMap.Add(0x405, "GLSL");
                extensionMap.Add(0x406, "TOT");
                extensionMap.Add(0x407, "TOH");
                extensionMap.Add(0x408, "MENU");
                extensionMap.Add(0x409, "LUA");
                extensionMap.Add(0x40a, "TTF");
                extensionMap.Add(0x44c, "BAH"); // ???????
                extensionMap.Add(0x802, "INI");
                extensionMap.Add(0x803, "SRC");

  //public static final int TYPE_MENU = 0x408;
  //      public static final int TYPE_LUA = 0x409;



    }

            return extensionMap;
        }
    }
}
