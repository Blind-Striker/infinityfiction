using System;
using System.Collections.Generic;

using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder.KeyResourceBundle
{
    public class KeyResourceBuilder : IKeyResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;
        private readonly IResourceConverter _resourceConverter;

        public KeyResourceBuilder(IGenericStructConverter genericStructConverter, IResourceConverter resourceConverter)
        {
            _genericStructConverter = genericStructConverter;
            _resourceConverter = resourceConverter;
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
    }
}
