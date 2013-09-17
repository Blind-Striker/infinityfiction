using System.Windows.Input;
using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels
{
    public class SelectGamePathViewModel : BaseViewModel
    {
        private string _gamePath;
        private string _description;

        private ICommand _select;
        private ICommand _cancel;
        private ICommand _browse;
        private ICommand _search;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        public string GamePath
        {
            get
            {
                return _gamePath;
            }
            set
            {
                if (_gamePath != value)
                {
                    _gamePath = value;
                    RaisePropertyChanged("GamePath");
                }
            }
        }

        public ICommand Select
        {
            get
            {
                return _select;
            }
            set
            {
                if (_select != value)
                {
                    _select = value;
                    RaisePropertyChanged("Select");
                }
            }
        }

        public ICommand Cancel
        {
            get
            {
                return _cancel;
            }
            set
            {
                if (_cancel != value)
                {
                    _cancel = value;
                    RaisePropertyChanged("Cancel");
                }
            }
        }

        public ICommand Browse
        {
            get
            {
                return _browse;
            }
            set
            {
                if (_browse != value)
                {
                    _browse = value;
                    RaisePropertyChanged("Browse");
                }
            }
        }

        public ICommand Search
        {
            get
            {
                return _search;
            }
            set
            {
                if (_search != value)
                {
                    _search = value;
                    RaisePropertyChanged("Search");
                }
            }
        }
    }
}
