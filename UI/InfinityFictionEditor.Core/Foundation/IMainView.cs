using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Foundation
{
    public interface IMainView : IPresenteredView<IMainPresenter>
    {
        void LoadModuleView(object view);
    }
}
