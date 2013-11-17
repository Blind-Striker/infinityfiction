using System;
using System.Linq;
using System.Windows.Forms;

using Castle.Core.Internal;

using InfinityFiction.UI.InfinityFictionEditor.Core;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;
using InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    public partial class MainForm : BaseForm, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public object DataContext
        {
            get
            {
                return dataContext.DataSource;
            }

            set
            {
                dataContext.DataSource = value;
            }
        }

        public IMainPresenter Presenter { get; set; }

        public void LoadModuleView(object view)
        {
            mainContainer.Panel2.Controls.Clear();

            Control control = view as Control;
            control.Dock = DockStyle.Fill;
            mainContainer.Panel2.Controls.Add(control);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var mainViewModel = DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                return;
            }

            Presenter.OnTreeViewItemsFilled += OnTreeViewItemsFilled;

            FillTreeView();

            CommandAdapter.AddCommandBinding(treeResources, mainViewModel.OnTreeItemSelected, () => mainViewModel.SelectedTreeViewItem = treeResources.SelectedNode.Tag);
            CommandAdapter.AddCommandBinding(selectGameToolStripMenuItem, mainViewModel.SelectGamePath);
        }

        private void OnTreeViewItemsFilled(object sender, EventArgs eventArgs)
        {
            FillTreeView();
        }

        private void FillTreeView()
        {
            var mainViewModel = DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                return;
            }

            treeResources.Nodes.Clear();

            var treeViewBinder = new TreeViewBinder();

            if (!mainViewModel.TreeViewItems.IsNullOrEmpty())
            {
                treeViewBinder.Bind(treeResources, mainViewModel.TreeViewItems);
            }
        }
    }
}
