using System;

namespace CodeFiction.InfinityFiction.Structure.StructConverterContracts
{
    public interface IGenericStructConverter
    {
        T ConvertToStruct<T>(byte[] content, int offset)
            where T : struct;
    }
}
