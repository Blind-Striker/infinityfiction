using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContracts;

namespace CodeFiction.InfinityFiction.Core.Services.Key
{
    public class KeyResourceService : IKeyResourceService
    {
        private readonly IKeyResourceBuilder _keyResourceBuilder;

        public KeyResourceService(IKeyResourceBuilder keyResourceBuilder)
        {
            _keyResourceBuilder = keyResourceBuilder;
        }

        public KeyResource GetKeyResource(GameEnum gameEnum, string keyfilePath)
        {
            return _keyResourceBuilder.GetKeyResource(gameEnum, keyfilePath);
        }
    }
}
