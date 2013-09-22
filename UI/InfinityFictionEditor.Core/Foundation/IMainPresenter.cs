using System;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Foundation
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        event EventHandler OnTreeViewItemsFilled;
    }
}
