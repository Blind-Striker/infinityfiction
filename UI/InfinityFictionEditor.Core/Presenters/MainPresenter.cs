using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Waf.Applications;

using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.Models;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Presenters
{
    internal class MainPresenter : BasePresenter<IMainView, IMainPresenter>, IMainPresenter
    {
        private readonly MainViewModel _mainViewModel;

        //public TreeViewItem SelectedSaveListItem
        //{
        //    get
        //    {
        //        if (_mainViewModel.TreeViewItems == null || _mainViewModel.TreeViewItems.Count == 0)
        //        {
        //            return null;
        //        }

        //        return _mainViewModel.TreeViewItems[_mainViewModel.CurrentTreeViewItemSelectedIndex];
        //    }
        //}

        public MainPresenter(IMainView view) 
            : base(view)
        {
            _mainViewModel = new MainViewModel();

            _mainViewModel.TreeViewItems = new ObservableCollection<TreeViewItem>();
            _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "1", Name = "ARE", ParentId = "" });
            _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "2", Name = "ASDETE.ARE", ParentId = "1" });
            _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "3", Name = "SDSAD.ARE", ParentId = "1" });
            _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "4", Name = "CHR", ParentId = "" });
            _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "5", Name = "DENIZ.CHR", ParentId = "4" });

            _mainViewModel.TreeItemSelected = new DelegateCommand(TreeItemSelected, CanTreeItemSelected);

            _mainViewModel.PropertyChanged += MainViewModelPropertyChanged;

            View.DataContext = _mainViewModel;
        }

        private void TreeItemSelected()
        {
        }

        private bool CanTreeItemSelected()
        {
            return true;
        }

        private void MainViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}
