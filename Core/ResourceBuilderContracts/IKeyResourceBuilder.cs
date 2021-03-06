using System;

using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilderContracts
{
    public interface IKeyResourceBuilder
    {
        KeyResource BuildKeyResource(byte[] content);

        KeyResource BuildKeyResourceNew(byte[] content);

        KeyResource BuildKeyResourceNew2(byte[] content, Type structType);
    }
}