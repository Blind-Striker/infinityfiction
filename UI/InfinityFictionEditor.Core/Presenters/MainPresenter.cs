using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Presenters
{
    internal class MainPresenter : BasePresenter<IMainView, IMainPresenter>, IMainPresenter
    {
        public MainPresenter(IMainView view) 
            : base(view)
        {
        }
    }
}
