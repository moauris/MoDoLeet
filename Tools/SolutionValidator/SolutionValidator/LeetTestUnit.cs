using System.Reflection;

namespace SolutionValidator
{
    public class LeetTestUnit
    {
        /// <summary>
        /// Initialize a Leetcode Soltion Test unit
        /// </summary>
        /// <param name="parameters">
        /// The input parameters of the test method.
        /// </param>
        /// <param name="answer">
        /// The answer to which the method was expected to produce.
        /// </param>
        public LeetTestUnit(Parms parameters, object answer)
        {
            Parameters = parameters;
            Answer = answer;
        }
        public Parms Parameters { get; private set; }
        public object Answer { get; private set; }
        /// <summary>
        /// Execute a Solution based on the parameters set in this object.
        /// </summary>
        /// <param name="Solution">
        /// The solution attached to this object
        /// </param>
        public void RunTest(object Solution) => SolutionTester.TestSolution(Solution, this);
    }
}