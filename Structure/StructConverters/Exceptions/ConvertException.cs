using System;
using System.Runtime.Serialization;

namespace CodeFiction.InfinityFiction.Structure.StructConverters.Exceptions
{
    public class ConvertException : StructConverterException
    {
        public ConvertException(string exceptionMessage, Exception exp)
            : base(exceptionMessage, exp)
        {
        }

        public ConvertException(Exception exp)
            : base(exp)
        {
        }

        public ConvertException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }

        public ConvertException()
        {
        }

        public ConvertException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
