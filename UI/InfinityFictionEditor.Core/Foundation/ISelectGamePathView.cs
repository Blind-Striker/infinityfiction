using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Foundation
{
    public interface ISelectGamePathView : IPresenteredView<ISelectGamePathPresenter>
    {
        void ShowDialog(object owner);

        void Close();
    }
}