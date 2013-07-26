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
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    public partial class MainForm : BaseForm, IMainView
    {
        public object DataContext { get { return dataContext.DataSource; } set { dataContext.DataSource = value; } }

        public IMainPresenter Presenter { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            MainViewModel mainViewModel = DataContext as MainViewModel;

            //CommandAdapter.AddCommandBinding(treeView1, mainViewModel.TreeItemSelected);
        }
    }
}
