using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolutionValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionValidator.Tests
{
    [TestClass()]
    public class SolutionTesterTests
    {
        [TestMethod()]
        public void TestSolutionTest()
        {
            LeetTestUnit unit = new LeetTestUnit(
                new object[]
                {
                    3, 3, 3
                }, 0);
            SolutionTester.TestSolution(new Solution(), unit);
        }
    }
    [LeetSolution]
    public class Solution
    {
        [LeetMethod]
        public int NumRollsToTarget(int d, int f, int target)
        {
            return 0;
        }
    }
}