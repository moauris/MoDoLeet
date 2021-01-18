using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SolutionValidator
{
    public class SolutionTester
    {
        public static int TestSolution(object solution
            , LeetTestUnit unit)
        {
            //Check if solution is a valid leetcode solution with attributes
            Type SolutionType = solution.GetType();
            bool IsLeetSolution = (SolutionType.GetCustomAttributes(
                typeof(LeetSolutionAttribute), false)).Any();
            Console.WriteLine($"The Solution class has Attribute: {IsLeetSolution}");
            if (!IsLeetSolution) return 100;
            //Searching for methods with LeetMethodAttribute
            var meths =
                from m in SolutionType.GetMethods()
                where m.GetCustomAttributes<LeetMethodAttribute>().Any()
                select m;
            bool ContainLeetMethod = false;
            ContainLeetMethod = meths.Count() > 0;
            Console.WriteLine($"The Solution class has Leet Method: {ContainLeetMethod}");
            if (!ContainLeetMethod) return 101;

            //Prepare is OK, start to check if LeetTestUnit fits the parameters

            ParameterInfo[] parms_this = meths.First().GetParameters();
            Parms parms_unit = unit.Parameters;
            //Checking if the count match
            int cnt_parms = parms_this.Count();
            int cnt_unit = parms_unit.Count();
            Console.WriteLine($"there are {cnt_parms} parms in solution, and {cnt_unit} parms in tester");
            if (cnt_parms != cnt_unit) return 102;
            //Checking if types match
            for (int i = 0; i < cnt_parms; i++)
            {
                Type t0 = parms_this[i].ParameterType;
                Type t1 = parms_unit[i].ParmType;

                Console.WriteLine($"Judging Parameter Types: {t0} = {t1} at index={i}");
                if (parms_this[i].ParameterType != parms_unit[i].ParmType)
                    return 103;
            }
            //Checking if Answer Type in unit is the return type of solution
            //foreach (MethodInfo m in meths)
            //{
            //    if (m.ReturnType != unit.Answer.GetType()) return 104;
            //}
            //All checks completed, starting testing:
            Console.WriteLine(" ╔═══════════════════════════════════╗");
            Console.WriteLine("   Leetcode Solution Tester Starts");
            Console.WriteLine(" ╚═══════════════════════════════════╝");

            // Invoking Solution and evaluate against the answer
            int count = 0;
            Stopwatch sw = new Stopwatch();
            foreach (MethodInfo m in meths)
            {
                Console.WriteLine();
                Console.WriteLine("Test {0} starts...", ++count);

                sw.Restart();
                object output = m.Invoke(solution, parms_unit.GetAllParmValues());
                sw.Stop();
#if DEBUG
                //Judging if the result is an int[]
                if (output is int[])
                {
                    string temp = string.Join(",", (int[])output);
                    output = "[" + temp + "]";
                }
#endif

                Console.WriteLine("Output => {0}", output);
                Console.WriteLine("Answer => {0}", unit.Answer);
                string resultIs = unit.Answer.ToString() == output.ToString() ? "correct" : "wrong";
                Console.WriteLine("The result is " + resultIs + ".");
                Console.WriteLine("Program ran for {0} ms.", sw.ElapsedMilliseconds);
                Console.WriteLine();
            }

            Console.WriteLine(" ╔════════════════════════════════════╗");
            Console.WriteLine("   Leetcode Solution Tester Finishes");
            Console.WriteLine(" ╚════════════════════════════════════╝");


#if xDEBUG
            foreach (MethodInfo m in meths)
            {
                ParameterInfo[] ps = m.GetParameters();
                foreach (ParameterInfo p in ps)
                {
                    Console.WriteLine("The Parameter Name is: " + p.Name);
                    Console.WriteLine("Is it out: " + p.IsOut);
                    Console.WriteLine("Is it in: " + p.IsIn);
                    Console.WriteLine("The Type of the parameter: "
                        + p.ParameterType.ToString());
                }
            }
#endif
            return 0;
        }
    }
}
