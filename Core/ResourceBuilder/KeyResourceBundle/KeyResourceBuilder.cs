using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder.KeyResourceBundle
{
    public class KeyResourceBuilder : IKeyResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;
        private readonly IResourceConverter _resourceConverter;
        private readonly IStructGenerator _structGenerator;

        public KeyResourceBuilder(IGenericStructConverter genericStructConverter, IResourceConverter resourceConverter, IStructGenerator structGenerator)
        {
            _genericStructConverter = genericStructConverter;
            _resourceConverter = resourceConverter;
            _structGenerator = structGenerator;
        }

        public KeyResource BuildKeyResource(byte[] content)
        {
            var keyResource = new KeyResource();

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
            _resourceConverter.Convert<ResourceEntry, ResourceEntryResource>(content, keyResource.ResourceEntries, resourceEntriesOffset, SizeOfResourceEntry);

            return keyResource;
        }

        public KeyResource BuildKeyResourceNew(byte[] content)
        {
            var keyResource = new KeyResource();

            int headerSize = Marshal.SizeOf(typeof(Header));
            byte[] headerContent = BinaryHelper.GetBytes(content, 0, (uint)headerSize);
            var header = _genericStructConverter.ConvertToStruct<Header>(headerContent, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;

            string keyStructCode = string.Format(Templates.KeyStructTemplate, biffEntriesOffset, resourceEntriesOffset, biffEntriesCount, resourceEntriesCount);

            Assembly generatedAssembly = _structGenerator.GenerateStruct(keyStructCode);
            Type structType = generatedAssembly.GetType("CodeFiction.InfinityFiction.Structure.Structures.Key.KeyStruct");
            object convertToStruct = _genericStructConverter.ConvertToStruct(structType, content, 0);

            var biffEntries = (Array)convertToStruct.GetType().GetField("BiffEntries").GetValue(convertToStruct);
            var resourceEntries = (Array)convertToStruct.GetType().GetField("ResourceEntries").GetValue(convertToStruct);

            keyResource.Header = new HeaderResource();
            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesCount];

            _resourceConverter.Convert(header, keyResource.Header);
            _resourceConverter.Convert(typeof(BiffEntry), biffEntries, keyResource.BiffEntries);
            _resourceConverter.Convert(typeof(ResourceEntry), resourceEntries, keyResource.ResourceEntries);

            return keyResource;
        }

        public KeyResource BuildKeyResourceNew2(byte[] content, Type structType)
        {
            var keyResource = new KeyResource();

            int headerSize = Marshal.SizeOf(typeof(Header));
            byte[] headerContent = BinaryHelper.GetBytes(content, 0, (uint)headerSize);
            var header = _genericStructConverter.ConvertToStruct<Header>(headerContent, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;

            object convertToStruct = _genericStructConverter.ConvertToStruct(structType, content, 0);

            var biffEntries = (Array)convertToStruct.GetType().GetField("BiffEntries").GetValue(convertToStruct);
            var resourceEntries = (Array)convertToStruct.GetType().GetField("ResourceEntries").GetValue(convertToStruct);

            keyResource.Header = new HeaderResource();
            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesCount];

            _resourceConverter.Convert(header, keyResource.Header);
            _resourceConverter.Convert(typeof(BiffEntry), biffEntries, keyResource.BiffEntries);
            _resourceConverter.Convert(typeof(ResourceEntry), resourceEntries, keyResource.ResourceEntries);

            return keyResource;
        }
    }
}
