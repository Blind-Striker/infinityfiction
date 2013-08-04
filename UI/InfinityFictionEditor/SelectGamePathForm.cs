using System;

using InfinityFiction.UI.InfinityFictionEditor.Core;
using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;
using InfinityFiction.UI.InfinityFictionEditor.Core.ViewModels;

namespace InfinityFiction.UI.InfinityFictionEditor
{
    public partial class SelectGamePathForm : BaseForm, ISelectGamePathView
    {
        public SelectGamePathForm()
        {
            InitializeComponent();
        }

        public object DataContext
        {
            get { return dataContext.DataSource; }
            set { dataContext.DataSource = value; }
        }

        public ISelectGamePathPresenter Presenter { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var gamePathViewModel = DataContext as SelectGamePathViewModel;

            CommandAdapter.AddCommandBinding(btnSelect, gamePathViewModel.Select);
            CommandAdapter.AddCommandBinding(btnCancel, gamePathViewModel.Cancel);
            CommandAdapter.AddCommandBinding(btnBrowse, gamePathViewModel.Browse);
            CommandAdapter.AddCommandBinding(btnSearch, gamePathViewModel.Search);
        }
    }
}
