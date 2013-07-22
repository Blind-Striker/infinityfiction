using System.Waf.Presentation.WinForms;
using System.Windows.Forms;

namespace InfinityFiction.UI.InfinityFictionEditor.Core
{
    public class BaseForm : Form
    {
        private readonly CommandAdapter _commandAdapter;

        public BaseForm()
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

        public void ShowDialog(object owner)
        {
            base.ShowDialog(owner as IWin32Window);
        }
    }
}
