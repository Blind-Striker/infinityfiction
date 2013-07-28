using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Presentation.WinForms;
using System.Windows.Forms;
using System.Windows.Input;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls.CommandBindings
{
    public class TreeViewBinding : CommandBindingBase
    {
        private TreeView _treeView;
        private Func<object> _commandParameterCallback;

        public TreeViewBinding(TreeView treeView, ICommand command, Func<object> commandParameterCallback)
            : base(treeView, command)
        {
            _treeView = treeView;
            _commandParameterCallback = commandParameterCallback;
            _treeView.AfterSelect += TreeViewAfterSelect;
        }

        protected override void OnCommandCanExecuteChanged()
        {
            Command.CanExecute(_commandParameterCallback());
        }

        protected override void OnComponentDisposed()
        {
            _treeView.AfterSelect -= TreeViewAfterSelect;
            _treeView = null;
            _commandParameterCallback = null;
        }

        void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            Command.Execute(_commandParameterCallback());
        }
    }
}
