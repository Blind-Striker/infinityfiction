namespace CodeFiction.InfinityFiction.Core.ServiceContracts
{
    public interface IResourceFileProvider
    {
        byte[] GetByteContentOfFile(string filePath);
    }
}