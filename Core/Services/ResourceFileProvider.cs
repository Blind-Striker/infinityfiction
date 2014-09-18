using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.ServiceContracts;

namespace CodeFiction.InfinityFiction.Core.Services
{
    public class ResourceFileProvider : IResourceFileProvider
    {
        public byte[] GetByteContentOfFile(string filePath)
        {
            byte[] content = IoHelper.ReadBinaryFile(filePath);

            return content;
        }
    }
}
