using CodeFiction.InfinityFiction.Core.Resources.Are;
using CodeFiction.InfinityFiction.Core.ServiceContracts;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;

namespace CodeFiction.InfinityFiction.Core.Services.Are
{
    public class AreService : IAreService
    {
        private readonly IGenericStructConverter _structBuilder;

        public AreService(IGenericStructConverter structBuilder)
        {
            _structBuilder = structBuilder;
        }

        public AreResource GetAreFile(byte[] content)
        {
            return null;
        }

        public AreResource GetAreFile(string name)
        {
            return null;
        }
    }
}
