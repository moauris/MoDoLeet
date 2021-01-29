using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolutionRunner.Components;

namespace SolutionRunner
{
    /// <summary>
    /// Solution Runner is a tool for running a Leetcode solutions with
    /// Parm result set stored in txt file.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner(typeof(Solution));


            Console.ReadLine();
        }

       
    }
    public class Solution
    {
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            return new TreeNode(0);
        }
    }

//Definition for a binary tree node.
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
    }

}
