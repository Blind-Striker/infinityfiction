using System.Collections.Generic;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Models
{
    public class TreeViewItem
    {
        public TreeViewItem()
        {
            TreeViewItems = new List<TreeViewItem>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public object Item { get; set; }

        public List<TreeViewItem> TreeViewItems { get; set; }
    }
}
