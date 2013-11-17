using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Castle.Windsor;

using InfinityFiction.UI.InfinityFictionEditor.Core.Exceptions;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.Modules.Module.Core;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core
{
    public class ModuleLoader : IModuleLoader
    {
        // TODO : The bad code here will be corrected later. IWindsorContainer will be removed.
        private readonly IWindsorContainer _container;

        public ModuleLoader(IWindsorContainer container)
        {
            _container = container;
        }

        public object LoadModule(string resourceType)
        {
            object presenter = _container.Resolve(resourceType, new Dictionary<string, object>());

            if (presenter is IPresenter 
                && presenter is IModulePresenter
                && ((IModulePresenter)presenter).FileType == resourceType)
            {

                PropertyInfo propertyInfo = presenter.GetType().GetProperty("View");

                MethodInfo methodInfo = propertyInfo.GetGetMethod();

                object view = methodInfo.Invoke(presenter, null);

                return view;
            }

            throw new ModuleLoaderException("Invalid Presenter. Module Presenters must derived from IPresenter and IModulePresenter");
        }
    }
}
