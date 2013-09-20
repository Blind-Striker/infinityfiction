using System;
using System.Configuration;
using System.IO;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;
using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Presenters
{
    public class SelectGamePathPresenter : BasePresenter<ISelectGamePathView, ISelectGamePathPresenter>, ISelectGamePathPresenter
    {
        private readonly SelectGamePathViewModel _gamePathViewModel;

        private readonly IDialogService _dialogService;

        private readonly IMessageService _messageService;

        private readonly ApplicationSettingsBase _settings;

        public SelectGamePathPresenter(ISelectGamePathView view, IDialogService dialogService, IMessageService messageService, ApplicationSettingsBase settings)
            : base(view)
        {
            _dialogService = dialogService;
            _messageService = messageService;
            _settings = settings;

            _gamePathViewModel = new SelectGamePathViewModel();

            _gamePathViewModel.GamePath = _settings["ChitinKeyPath"].ToString();

            _gamePathViewModel.Select = new DelegateCommand(Select, CanSelect);
            _gamePathViewModel.Cancel = new DelegateCommand(Cancel);
            _gamePathViewModel.Browse = new DelegateCommand(Browse);
            _gamePathViewModel.Search = new DelegateCommand(Search);

            View.DataContext = _gamePathViewModel;
        }

        private void Select()
        {
            _settings["ChitinKeyPath"] = _gamePathViewModel.GamePath;

            _settings.Save();

            View.Close();
        }

        private bool CanSelect()
        {
           return !_gamePathViewModel.GamePath.IsNullOrEmpty() && File.Exists(_gamePathViewModel.GamePath);
        }

        private void Cancel()
        {
            View.Close();
        }

        private void Browse()
        {
            string selectedPath = _dialogService.ShowFileDialog(View, "Infinity Keyfile (*.key)|*.key|All files (*.*)|*.*");

            if (!selectedPath.IsNullOrEmpty())
            {
                _gamePathViewModel.GamePath = selectedPath;
            }

            var delegateCommand = _gamePathViewModel.Select as DelegateCommand;
            if (delegateCommand != null)
            {
                delegateCommand.RaiseCanExecuteChanged();
            }
        }

        private void Search()
        {
            _messageService.ShowMessage("Soon...");
        }
    }
}
