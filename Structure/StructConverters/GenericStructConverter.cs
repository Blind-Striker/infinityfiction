using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.StructConverters.Exceptions;

namespace CodeFiction.InfinityFiction.Structure.StructConverters
{
    public class GenericStructConverter : IGenericStructConverter
    {
        public T ConvertToStruct<T>(byte[] content, int startIndex) where T : struct
        {
            Type structType = typeof(T);
            object convertToStruct = ConvertToStruct(structType, content, startIndex);

            return (T)convertToStruct;
        }

        public object ConvertToStruct(Type structType, byte[] content, int startIndex)
        {
            // TODO : design by contract
            if (content == null || !content.Any())
            {
                throw new StructConverterException("Content cannot be null or empty");
            }

            if (startIndex < 0 || content.Length <= startIndex)
            {
                throw new StructConverterException("startIndex must bigger then zero and smaller then content Length");
            }

            try
            {
                IntPtr unmanagedPointer = Marshal.AllocHGlobal(content.Length);
                Marshal.Copy(content, startIndex, unmanagedPointer, content.Length);

                var entity = Marshal.PtrToStructure(unmanagedPointer, structType);

                Marshal.FreeHGlobal(unmanagedPointer);

                return entity;
            }
            catch (Exception ex)
            {
                throw new ConvertException(string.Format("An error occured while reading {0}.", structType.FullName), ex);
            }
        }
    }
}
