using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using InfinityFiction.UI.InfinityFictionEditor.Core.Models;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.WinFormControls
{
    public class TreeViewBinder
    {
        public void Bind(TreeView treeView, IEnumerable<TreeViewItem> items)
        {
            var nodes = new List<TreeNode>();

            foreach (var treeViewItem in items)
            {
                var node = new TreeNode(treeViewItem.Name) { Tag = treeViewItem.Item };

                if (!treeViewItem.TreeViewItems.IsNullOrEmpty())
                {
                    FillRecursively(node, treeViewItem.TreeViewItems);
                }

                nodes.Add(node);
            }

            treeView.Nodes.AddRange(nodes.ToArray());
        }

        private void FillRecursively(TreeNode treeNode, IEnumerable<TreeViewItem> items)
        {
            foreach (var treeViewItem in items)
            {
                TreeNode childNode = new TreeNode(treeViewItem.Name) { Tag = treeViewItem.Item };

                if (!treeViewItem.TreeViewItems.IsNullOrEmpty())
                {
                    FillRecursively(childNode, treeViewItem.TreeViewItems);
                }

                treeNode.Nodes.Add(childNode);
            }
        }
    }
}
