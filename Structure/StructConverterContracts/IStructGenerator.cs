using System.Reflection;

namespace CodeFiction.InfinityFiction.Structure.StructConverterContracts
{
    public interface IStructGenerator
    {
        Assembly GenerateStruct(string structText);
    }
}