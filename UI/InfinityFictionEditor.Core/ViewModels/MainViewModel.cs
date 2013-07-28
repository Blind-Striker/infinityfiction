using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using InfinityFiction.UI.InfinityFictionEditor.Core.Models;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _selectedTreeViewItemId;
        private ObservableCollection<TreeViewItem> _treeViewItems;
        private ICommand _onTreeItemSelected;
        //private int _currentTreeViewItemSelectedIndex;

        public ObservableCollection<TreeViewItem> TreeViewItems
        {
            get { return _treeViewItems; }
            set
            {
                _treeViewItems = value;
                RaisePropertyChanged("TreeViewItems");
            }
        }

        public object SelectedTreeViewItem { get; set; }

        public ICommand OnTreeItemSelected
        {
            get { return _onTreeItemSelected; }
            set
            {
                _onTreeItemSelected = value;
                RaisePropertyChanged("OnTreeItemSelected");
            }
        }

        //public int CurrentTreeViewItemSelectedIndex
        //{
        //    get { return _currentTreeViewItemSelectedIndex; }
        //    set
        //    {
        //        _currentTreeViewItemSelectedIndex = value;
        //        RaisePropertyChanged("CurrentTreeViewItemSelectedIndex");
        //    }
        //}
    }
}
