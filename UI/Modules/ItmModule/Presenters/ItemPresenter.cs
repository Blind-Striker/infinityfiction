using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeFiction.InfinityFiction.Core.CommonTypes;

using InfinityFiction.UI.Modules.ItmModule.Foundation;
using InfinityFiction.UI.Modules.Module.Core;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.Modules.ItmModule.Presenters
{
    public class ItemPresenter : BasePresenter<IItemView, IItemPresenter>, IItemPresenter, IModulePresenter
    {
        public ItemPresenter(IItemView view)
            : base(view)
        {
        }

        public string FileType { get {return "ITM";} }

        public void LoadResourceFile(ResourceFile resourceFile)
        {

        }
    }
}
