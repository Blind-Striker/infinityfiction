using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Presentation.WinForms;
using System.Windows.Forms;
using System.Windows.Input;

using InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls.CommandBindings;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls.CommandFactories
{
    public class TreeViewCommandBindingFactory : CommandBindingFactory
    {
        protected override bool CanCreateCore(Component component)
        {
            return component is TreeView;
        }

        protected override CommandBindingBase CreateCore(Component component, ICommand command, Func<object> commandParameterCallback)
        {
            TreeView treeView = component as TreeView;
            if (treeView == null)
            {
                throw new ArgumentException("This factory cannot create a CommandBindingBase for the passed component.");
            }
            return new TreeViewBinding(treeView, command, commandParameterCallback);
        }
    }
}
