using System;
using System.Runtime.Serialization;
using CodeFiction.DarkMatterFramework.CoreExceptions;

namespace CodeFiction.InfinityFiction.Structure.StructConverters.Exceptions
{
    public class StructConverterException : CoreLevelException
    {
        public StructConverterException(string exceptionMessage, Exception exp)
            : base(exceptionMessage, exp)
        {
        }

        public StructConverterException(Exception exp)
            : base(exp)
        {
        }


        public StructConverterException(string exceptionMessage)
            : base(exceptionMessage)
        {
        }


        public StructConverterException()
        {        
        }


        public StructConverterException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
