using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

using CodeFiction.InfinityFiction.Structure.StructConverters;
using CodeFiction.InfinityFiction.Structure.StructConverters.Exceptions;

using StructConverters.Tests.TestClassess;

using Xunit;

namespace StructConverters.Tests
{
    public class GenericStructConverterTests
    {
        [Fact]
        public void ConvertToStruct_Should_Throw_StructConverterException_When_Content_Parameter_Null_Or_Empty()
        {
            var genericStructConverter = new GenericStructConverter();

            Assert.Throws<StructConverterException>(() => genericStructConverter.ConvertToStruct<TestStruct>(null, 0));
            Assert.Throws<StructConverterException>(() => genericStructConverter.ConvertToStruct<TestStruct>(new byte[0], 0));
        }

        [Fact]
        public void ConvertToStruct_Should_Throw_StructConverterException_When_StartIndex_Smaller_Then_Zero_Or_BiggerEq_Then_Content_Length()
        {
            var genericStructConverter = new GenericStructConverter();

            var bytes = new byte[] { 1 };

            Assert.Throws<StructConverterException>(() => genericStructConverter.ConvertToStruct<TestStruct>(bytes, -1));
            Assert.Throws<StructConverterException>(() => genericStructConverter.ConvertToStruct<TestStruct>(bytes, bytes.Length + 1));
        }
    }
}
