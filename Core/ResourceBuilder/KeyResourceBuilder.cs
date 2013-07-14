using System;
using System.IO;
using System.Reflection;

using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.Utilities;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class KeyResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;

        public KeyResourceBuilder(IGenericStructConverter genericStructConverter)
        {
            _genericStructConverter = genericStructConverter;
        }

        public KeyResource GetKeyResource()
        {
            var keyResource = new KeyResource();

            string savefile = Path.Combine(@"G:\Games\BGOrg\BGII - SoA", "CHITIN.KEY");
            byte[] content = IoHelper.ReadBinaryFile(savefile);

            var header = _genericStructConverter.ConvertToStruct<Header>(content, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;

            var biffEntryResources = new BiffEntry[biffEntriesCount];
            var resourceEntries = new ResourceEntry[resourceEntriesCount];
            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesOffset];

            for (int i = 0; i < biffEntriesCount; i++)
            {
                byte[] biffEntryContent = BinaryHelper.GetBytes(content, (int)biffEntriesOffset, 12);

                var biffEntry = _genericStructConverter.ConvertToStruct<BiffEntry>(biffEntryContent, 0);

                biffEntryResources[i] = biffEntry;

                biffEntriesOffset += 12;
            }

            for (int i = 0; i < resourceEntriesCount; i++)
            {
                byte[] resourceEntryContent = BinaryHelper.GetBytes(content, (int)resourceEntriesOffset, 14);

                var resourceEntry = _genericStructConverter.ConvertToStruct<ResourceEntry>(resourceEntryContent, 0);

                resourceEntries[i] = resourceEntry;

                resourceEntriesOffset += 14;
            }

            keyResource.Header = new HeaderResource();

            FieldInfo[] fieldInfos = header.GetType().GetFields();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Delegate getter = DelegateHelper.CreateGetter(fieldInfo);
                object value = getter.DynamicInvoke(header);

                keyResource.Header.Properties.Add(new SimpleDataType { Name = fieldInfo.Name, Value = value });
            }

            FieldInfo[] fields = typeof(BiffEntry).GetFields();

            for (int i = 0; i < biffEntryResources.Length; i++)
            {
                BiffEntry biffEntry = biffEntryResources[i];
                keyResource.BiffEntries[i] = new BiffEntryResource();

                foreach (FieldInfo fieldInfo in fields)
                {
                    Delegate getter = DelegateHelper.CreateGetter(fieldInfo);
                    object value = getter.DynamicInvoke(biffEntry);

                    keyResource.BiffEntries[i].Properties.Add(new SimpleDataType { Name = fieldInfo.Name, Value = value });
                }
            }

            return keyResource;
        }
    }
}
