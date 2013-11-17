using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using InfinityFiction.UI.Modules.ItmModule.Foundation;

namespace InfinityFiction.UI.Modules.ItmModule
{
    public partial class ItemUserControl : UserControl, IItemView 
    {
        public ItemUserControl()
        {
            InitializeComponent();
        }

        public IItemPresenter Presenter { get; set; }

        public object DataContext
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}
