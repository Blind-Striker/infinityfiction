using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<TreeViewItem> _treeViewItems;
        private ICommand _treeItemSelected;
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

        public ICommand TreeItemSelected
        {
            get { return _treeItemSelected; }
            set
            {
                _treeItemSelected = value;
                RaisePropertyChanged("TreeItemSelected");
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
