using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Static
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// recusrive node search over a whole TreeView
        /// </summary>
        public static IEnumerable<TreeNode> FindNodesWhere(this TreeView treeView, Func<TreeNode, bool> criteria) =>
            FindNodesWhere(treeView.Nodes.OfType<TreeNode>(), criteria);
        
        /// <summary>
        /// recursive node search over a set of nodes
        /// </summary>
        public static IEnumerable<TreeNode> FindNodesWhere(this IEnumerable<TreeNode> nodes, Func<TreeNode, bool> criteria)
        {
            List<TreeNode> results = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                FindNodesInner(node, results, criteria);
            }

            return results;
        }

        public static IEnumerable<TreeNode> FindNodesOfType<T>(this TreeView treeView) where T : TreeNode
        {
            List<T> results = new List<T>();
            results.AddRange(FindNodesOfType<T>(treeView.Nodes.OfType<TreeNode>()));
            return results;
        }

        public static IEnumerable<T> FindNodesOfType<T>(this IEnumerable<TreeNode> nodes) where T : TreeNode
        {
            List<T> results = new List<T>();

            foreach (TreeNode node in nodes)
            {
                FindNodesOfTypeInner(node);
            }

            return results;

            void FindNodesOfTypeInner(TreeNode node)
            {
                results.AddRange(node.Nodes.OfType<T>());
                foreach (TreeNode child in node.Nodes) FindNodesOfTypeInner(child);
            }
        }

        /// <summary>
        /// recursive node search starting from a single node
        /// </summary>
        public static IEnumerable<TreeNode> FindNodesWhere(this TreeNode node, Func<TreeNode, bool> criteria)
        {
            List<TreeNode> results = new List<TreeNode>();

            foreach (TreeNode child in node.Nodes)
            {
                FindNodesInner(node, results, criteria);
            }

            return results;
        }

        private static void FindNodesInner(this TreeNode node, List<TreeNode> results, Func<TreeNode, bool> criteria)
        {
            foreach (TreeNode child in node.Nodes)
            {
                if (criteria.Invoke(child)) results.Add(child);
                FindNodesInner(child, results, criteria);
            }
        }
    }
}
