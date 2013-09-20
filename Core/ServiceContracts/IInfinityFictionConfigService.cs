using System.Collections.Generic;

using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;

namespace CodeFiction.InfinityFiction.Core.ServiceContracts
{
    public interface IInfinityFictionConfigService
    {
        KeyResource KeyResource { get; }
        IList<ResourceFile> ResourceFiles { get; }

        void InitializeConfiguration(string keyFilePath);
    }
}