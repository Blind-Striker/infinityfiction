using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilderContracts
{
    public interface IKeyResourceBuilder
    {
        KeyResource GetKeyResource(GameEnum gameEnum, string keyFilePath);
    }
}