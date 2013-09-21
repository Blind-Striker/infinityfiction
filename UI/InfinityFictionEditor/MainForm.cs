using System;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var mainViewModel = DataContext as MainViewModel;

            if (mainViewModel == null)
            {
                return;
            }

            var treeViewBinder = new TreeViewBinder();

            if (!mainViewModel.TreeViewItems.IsNullOrEmpty())
            {
                treeViewBinder.Bind(treeResources, mainViewModel.TreeViewItems);
            }

            CommandAdapter.AddCommandBinding(treeResources, mainViewModel.OnTreeItemSelected, () => mainViewModel.SelectedTreeViewItem = treeResources.SelectedNode.Tag);
            CommandAdapter.AddCommandBinding(selectGameToolStripMenuItem, mainViewModel.SelectGamePath);
        }
    }
}
