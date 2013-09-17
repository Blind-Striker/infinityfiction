using System.Collections.ObjectModel;
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
        private ICommand _selectGamePath;

        public ObservableCollection<TreeViewItem> TreeViewItems
        {
            get
            {
                return _treeViewItems;
            }
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

        public ICommand SelectGamePath
        {
            get { return _selectGamePath; }
            set
            {
                if (_selectGamePath != value)
                {
                    _selectGamePath = value;
                    RaisePropertyChanged("SelectGamePath");
                }
            }
        }
    }
}
