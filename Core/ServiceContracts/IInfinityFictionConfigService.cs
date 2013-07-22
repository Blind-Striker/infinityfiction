using CodeFiction.InfinityFiction.Core.Resources.Key;

namespace CodeFiction.InfinityFiction.Core.ServiceContracts
{
    public interface IInfinityFictionConfigService
    {
        KeyResource KeyResource { get; }

        void InitializeConfiguration(string keyFilePath);
    }
}