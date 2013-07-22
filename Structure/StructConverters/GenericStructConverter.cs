using System;
using System.Runtime.InteropServices;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.StructConverters.Exceptions;

namespace CodeFiction.InfinityFiction.Structure.StructConverters
{
    public class GenericStructConverter : IGenericStructConverter
    {
        public T ConvertToStruct<T>(byte[] content, int offset) where T : struct
        {
            try
            {
                IntPtr unmanagedPointer = Marshal.AllocHGlobal(content.Length);
                Marshal.Copy(content, offset, unmanagedPointer, content.Length);

                var header = (T)Marshal.PtrToStructure(unmanagedPointer, typeof(T));

                Marshal.FreeHGlobal(unmanagedPointer);

                return header;
            }
            catch (Exception ex)
            {
                throw new ConvertException(string.Format("An error occured while reading {0}.", typeof(T).FullName), ex);
            }
        }
    }
}
