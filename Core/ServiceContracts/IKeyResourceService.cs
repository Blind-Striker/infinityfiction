using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;

namespace CodeFiction.InfinityFiction.Core.ServiceContracts
{
    public interface IKeyResourceService
    {
        KeyResource GetKeyResource(GameEnum gameEnum, string keyfilePath);
    }
}