using GraphSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphSearch
{
    public class TreeUpdater
    {
        TreeView tree;
        TreeViewItem CanvasItem = new TreeViewItem();
        public TreeUpdater(TreeView tree) 
        {
           // CanvasItem = new TreeViewItem();
            CanvasItem.Header = "Canvas";
            this.tree = tree;
            tree.Items.Add(CanvasItem);
        }
        public void UpdateTree(TreeViewItem node = CanvasItem, IShape shape)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = shape.Name;
            node.Items.Add(item);

            if (shape is ShapeGroup)
            {
                foreach (var s in (shape as ShapeGroup).shapes)
                {
                    UpdateTree(item, s);
                }
            }
        }
    }
}
