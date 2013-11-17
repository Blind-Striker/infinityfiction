using System.Waf.Presentation.WinForms;
using System.Windows.Forms;

namespace InfinityFiction.UI.Modules.Module.Core
{
    public partial class BaseUserControl : UserControl
    {
        private readonly CommandAdapter _commandAdapter;

        public BaseUserControl()
        {
            _commandAdapter = new CommandAdapter();
        }

        protected CommandAdapter CommandAdapter
        {
            get
            {
                return _commandAdapter;
            }
        }
    }
}
