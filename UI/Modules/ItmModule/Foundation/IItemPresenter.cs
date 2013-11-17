using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InfinityFiction.UI.Modules.ItmModule.Presenters;
using InfinityFiction.UI.Modules.Module.Core;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.Modules.ItmModule.Foundation
{
    public interface IItemPresenter : IPresenter<IItemView> 
    {
    }
}
