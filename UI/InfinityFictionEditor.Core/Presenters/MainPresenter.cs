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
        public event EventHandler OnTreeViewItemsFilled;

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

            _mainViewModel.TreeViewItems = new ObservableCollection<TreeViewItem>();

            if (_settings["ChitinKeyPath"] == null)
            {
                ShowSelectGamePathView();
            }
            else
            {
                InitializeKeyResource();
            }
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

        private bool CanShowSelectGamePathView()
        {
            return true;
        }

        private void ShowSelectGamePathView()
        {
            var selectGamePathPresenter = _presenterFactory.CreatePresenter<ISelectGamePathPresenter>();

            selectGamePathPresenter.View.ShowDialog(View);

            InitializeKeyResource();
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
            _mainViewModel.TreeViewItems.Clear();
            List<string> resourceFolders = _resourceFiles.Select(file => file.RootFolder).OrderBy(s => s).Distinct().ToList();

            // TODO : The bad code here will be corrected later.
            foreach (var resourceFolder in resourceFolders)
            {               
                List<ResourceFile> files = _resourceFiles.Where(file => file.Folder == resourceFolder).OrderBy(file => file.File).ToList();
                List<ResourceFile> fileInFileSystem = _resourceFiles.Where(file => file.RootFolder == resourceFolder && file.RootFolder != file.Folder).OrderBy(file => file.File).ToList();
                List<string> foldersInFileSystem = fileInFileSystem.Select(file => file.Folder).OrderBy(s => s).Distinct().ToList();

                TreeViewItem parentTreeItem = new TreeViewItem()
                                            {
                                                Id = resourceFolder,
                                                Name = string.Format("{0} - {1}", resourceFolder, files.Count + foldersInFileSystem.Count),
                                                ParentId = string.Empty,
                                                Item = null
                                            };

                foreach (var folder in foldersInFileSystem)
                {
                    List<ResourceFile> resourceFilesInFileSystem = fileInFileSystem.Where(file => file.Folder == folder).ToList();

                    TreeViewItem folderTreeItem = new TreeViewItem()
                    {
                        Id = folder,
                        Name = string.Format("{0} - {1}", folder, resourceFilesInFileSystem.Count),
                        ParentId = string.Empty,
                        Item = null
                    };

                    foreach (var resourceFile in resourceFilesInFileSystem)
                    {
                        folderTreeItem.TreeViewItems.Add(new TreeViewItem { Id = resourceFile.File, Name = resourceFile.File, ParentId = folder, Item = resourceFile });
                    }

                    parentTreeItem.TreeViewItems.Add(folderTreeItem);
                }

                foreach (var resourceFile in files)
                {
                    parentTreeItem.TreeViewItems.Add(new TreeViewItem {Id = resourceFile.File, Name = resourceFile.File, ParentId = resourceFolder, Item = resourceFile});
                }

                _mainViewModel.TreeViewItems.Add(parentTreeItem);
            }

            if (OnTreeViewItemsFilled != null)
            {
               OnTreeViewItemsFilled(this, new EventArgs());
            }
        }


        private void MainViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}
