using System;

using CodeFiction.InfinityFiction.Core.Resources;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilderContracts
{
    public interface IResourceConverter
    {
        void Convert<TStruct, TResource>(TStruct @struct, TResource resource)
            where TResource : BaseModel;

        void Convert<TStruct, TResource>(byte[] content, TResource[] resources, uint offset, uint sizeofStruct, Action<TResource> onResourceConverted = null)
            where TStruct : struct 
            where TResource : BaseModel, new();
    }
}