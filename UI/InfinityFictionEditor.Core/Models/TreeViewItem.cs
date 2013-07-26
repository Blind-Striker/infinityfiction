using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Models
{
    public class TreeViewItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }
    }
}
