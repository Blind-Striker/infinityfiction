using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Waf.Applications;

using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContracts;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.Models;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;
using MvpVmFramework.Core.Foundation;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Presenters
{
    internal class MainPresenter : BasePresenter<IMainView, IMainPresenter>, IMainPresenter
    {
        private readonly IAppSettings _settings;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IInfinityFictionConfigService _infinityFictionConfigService;
        private readonly MainViewModel _mainViewModel;
        private KeyResource _keyResource;
        private IList<ResourceFile> _resourceFiles;

        public MainPresenter(
            IMainView view,
            IAppSettings settings,
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

            InitializeKeyResource();

            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "1", Name = "ARE", ParentId = "" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "2", Name = "ASDETE.ARE", ParentId = "1" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "3", Name = "SDSAD.ARE", ParentId = "1" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "4", Name = "CHR", ParentId = "" });
            // _mainViewModel.TreeViewItems.Add(new TreeViewItem { Id = "5", Name = "DENIZ.CHR", ParentId = "4" });
        }

        public ResourceFile SelectedResourceFile
        {
            get
            {
                return _mainViewModel.SelectedTreeViewItem as ResourceFile;
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

            InitializeKeyResource();
        }

        private bool CanShowSelectGamePathView()
        {
            return true;
        }

        private void CheckShowSelectGamePathView()
        {
            if (_settings["ChitinKeyPath"] == null)
            {
                ShowSelectGamePathView();
            }
        }

        private void MainViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void InitializeKeyResource()
        {
            object chitinKeyPath = _settings["ChitinKeyPath"];
            if (chitinKeyPath != null)
            {
                _infinityFictionConfigService.InitializeConfiguration(chitinKeyPath.ToString());
                _keyResource = _infinityFictionConfigService.KeyResource;
                _resourceFiles = _infinityFictionConfigService.ResourceFiles;
                PopulateResourceTree();
            }
        }

        private void PopulateResourceTree()
        {
            _mainViewModel.TreeViewItems = new ObservableCollection<TreeViewItem>();
            List<ResourceFile> resourceFiles = _resourceFiles.Where(file => file.ParentFolder == null).ToList();
            List<string> resourceFolders = resourceFiles.Select(file => file.Folder).OrderBy(s => s).Distinct().ToList();

            foreach (var resourceFolder in resourceFolders)
            {
                List<ResourceFile> files = resourceFiles.Where(file => file.Folder == resourceFolder).OrderBy(file => file.File).ToList();

                TreeViewItem parentTreeItem = new TreeViewItem()
                                            {
                                                Id = resourceFolder,
                                                Name = string.Format("{0} - {1}", resourceFolder, files.Count),
                                                ParentId = string.Empty,
                                                Item = null
                                            };

                foreach (var resourceFile in files)
                {
                    parentTreeItem.TreeViewItems.Add(new TreeViewItem() { Id = resourceFile.File, Name = resourceFile.File, ParentId = resourceFolder, Item = resourceFile});
                }

                _mainViewModel.TreeViewItems.Add(parentTreeItem);
            }
        }
    }
}
