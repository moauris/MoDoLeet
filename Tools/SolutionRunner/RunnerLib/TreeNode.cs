using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionRunner.Components
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
        /// <summary>
        /// Convert the Tree to int array
        /// </summary>
        /// <returns>
        /// The int array representing BST flattened
        /// </returns>
        public int[] Flatten()
        {
            List<int> res = new List<int>();
            printLevelOrder(this, ref res);
            return res.ToArray();
        }
        public string FlattenToString() => "[" + string.Join(",", Flatten()) + "]";

        /// <summary>Returns the height of the BST</summary>
        /// <param name="root">The root node of the tree whose height is to be calculated</param>
        /// <returns>The height of the tree</returns>
        private int height(TreeNode root)
        {
            if (root == null) return 0;
            else
            {
                int left = height(root.left);
                int right = height(root.right);
                // return the + 1 of the larger val;
                return Math.Max(left, right) + 1;
            }
        }
        private void printGivenLevel(TreeNode root, int level, ref List<int> list)
        {
            if (root == null) return;
            if (level == 1) list.Add(root.val);
            else if (level > 1)
            {
                printGivenLevel(root.left, level - 1, ref list);
                printGivenLevel(root.right, level - 1, ref list);
            }
        }
        private void printLevelOrder(TreeNode root, ref List<int> list)
        {
            int h = height(root);
            for (int i = 1; i < h; i++)
                printGivenLevel(root, i, ref list);
        }
    }
}
