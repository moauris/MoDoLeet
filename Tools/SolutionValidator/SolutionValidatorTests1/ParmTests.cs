using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolutionValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionValidator.Tests
{
    [TestClass()]
    public class ParmTests
    {
        private static Parm p1 = new Parm(3449);
        private static Parm p2 = new Parm(2269m);
        private static Parm p3 = new Parm(3449m);
        private static Parm p4 = new Parm(3449);
        [TestMethod()]
        public void EqualsTest()
        {
            Console.WriteLine(p1 == p2);
            Console.WriteLine(p1 == p3);
            Console.WriteLine(p1 == p4);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            int h1 = p1.GetHashCode();
            int h2 = p2.GetHashCode();
            int h3 = p3.GetHashCode();
            int h4 = p4.GetHashCode();

            Console.WriteLine($"h1 = {h1}");
            Console.WriteLine($"h2 = {h2}");
            Console.WriteLine($"h3 = {h3}");
            Console.WriteLine($"h4 = {h4}");
        }
    }
}