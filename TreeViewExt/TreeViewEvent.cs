using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotnet.common.TreeViewExt
{
    public class TreeViewEvent
    {
        /// <summary>
        /// 当节点选中状态改变时，更新所有子节点和单链父节点状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void treeViewEvent_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Checked)
                {
                    UpdateChildNodeCheckedState(e.Node, true);
                }
                else
                {
                    UpdateChildNodeCheckedState(e.Node, false);
                    UpdateParentNodeCheckedState(e.Node, false);
                }
            }
        }

        /// <summary>
        /// 更新父节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        private static void UpdateParentNodeCheckedState(TreeNode node, bool state)
        {
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                parent.Checked = state;
                UpdateParentNodeCheckedState(parent, state);
            }
        }

        /// <summary>
        /// 更新子节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        private static void UpdateChildNodeCheckedState(TreeNode node, bool state)
        {
            TreeNodeCollection childNodes = node.Nodes;
            if (childNodes.Count > 0)
            {
                foreach (TreeNode childNode in childNodes)
                {
                    childNode.Checked = state;
                    UpdateChildNodeCheckedState(childNode, state);
                }
            }
        }


        public static HashSet<string> GetSelectedLeafNodeValues(TreeView treeView)
        {
            HashSet<string> leafValues = new HashSet<string>();
            foreach (TreeNode node in treeView.Nodes)
            {
                AddSelectedLeafNodeValues(node, leafValues);
            }
            return leafValues;
        }

        private static void AddSelectedLeafNodeValues(TreeNode node, HashSet<string> leafValues)
        {
            if (node.Nodes.Count == 0)
            {
                if (node.Checked)
                    leafValues.Add(node.Text);
            }
            else
            {
                foreach (TreeNode childNode in node.Nodes)
                    AddSelectedLeafNodeValues(childNode, leafValues);
            }

        }
    }
}
