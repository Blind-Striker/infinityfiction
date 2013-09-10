using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Waf.Applications;

using CodeFiction.InfinityFiction.Core.ServiceContracts;

using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.Models;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;

using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Presenters
{
    internal class MainPresenter : BasePresenter<IMainView, IMainPresenter>, IMainPresenter
    {
        private readonly ApplicationSettingsBase _settings;

        private readonly IPresenterFactory _presenterFactory;

        private readonly IInfinityFictionConfigService _infinityFictionConfigService;

        private readonly MainViewModel _mainViewModel;

        public MainPresenter(
            IMainView view, 
            ApplicationSettingsBase settings, 
            IPresenterFactory presenterFactory, 
            IInfinityFictionConfigService infinityFictionConfigService) 
            : base(view)
        {
            _settings = settings;
            _presenterFactory = presenterFactory;
            _infinityFictionConfigService = infinityFictionConfigService;
            _mainViewModel = new MainViewModel
                {
                    OnTreeItemSelected = new DelegateCommand(TreeItemSelected, CanTreeItemSelected),
                    SelectGamePath = new DelegateCommand(ShowSelectGamePathView, CanShowSelectGamePathView)
                };
            _mainViewModel.PropertyChanged += MainViewModelPropertyChanged;

            View.DataContext = _mainViewModel;

            CheckShowSelectGamePathView();


            // _mainViewModel.TreeViewItems = new ObservableCollection<TreeViewItem>();
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "1", Name = "ARE", ParentId = "" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "2", Name = "ASDETE.ARE", ParentId = "1" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "3", Name = "SDSAD.ARE", ParentId = "1" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "4", Name = "CHR", ParentId = "" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "5", Name = "DENIZ.CHR", ParentId = "4" });
        }

        public TreeViewItem SelectedTreeViewItem
        {
            get
            {
                return _mainViewModel.SelectedTreeViewItem as TreeViewItem;
            }
        }

        private void TreeItemSelected()
        {

        }

        private bool CanTreeItemSelected()
        {
            return true;
        }

        private void ShowSelectGamePathView()
        {
            var selectGamePathPresenter = _presenterFactory.CreatePresenter<ISelectGamePathPresenter>();

            selectGamePathPresenter.View.ShowDialog(View);
        }

        private bool CanShowSelectGamePathView()
        {
            return true;
        }

        private void CheckShowSelectGamePathView()
        {
            if (_settings["ChitinKeyPath"].ToString().IsNullOrEmpty())
            {
                ShowSelectGamePathView();
            }
        }

        private void MainViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}
