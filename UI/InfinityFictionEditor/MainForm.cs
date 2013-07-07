using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InfinityFiction.UI.InfinityFictionEditor.Core;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;


namespace InfinityFiction.UI.InfinityFictionEditor
{
    public partial class MainForm : BaseForm, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public object DataContext { get; set; }

        public IMainPresenter Presenter { get; set; }
    }
}
