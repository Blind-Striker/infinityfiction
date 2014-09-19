using System;

namespace CodeFiction.InfinityFiction.Structure.StructConverterContracts
{
    public interface IGenericStructConverter
    {
        T ConvertToStruct<T>(byte[] content, int startIndex)
            where T : struct;

        object ConvertToStruct(Type structType, byte[] content, int startIndex);
    }
}
