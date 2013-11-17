using System.Waf.Applications;

using CodeFiction.InfinityFiction.Core.CommonTypes;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.Modules.Module.Core
{
    public interface IModulePresenter
    {
        string FileType { get; }

        void LoadResourceFile(ResourceFile resourceFile);
    }
}
