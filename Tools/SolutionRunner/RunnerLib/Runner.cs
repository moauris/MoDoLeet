using System;
using System.Linq;
using System.Reflection;

namespace SolutionRunner.Components
{
    public class Runner
    {
        public Runner(Type solution)
        {
#if DEBUG
            Console.WriteLine($"The Type of Solution is: {solution.Name}");
#endif
            //需要筛出object中含有的4个方法以及private方法
            var ObjectMethods =
                from m in typeof(object).GetMethods()
                select m.Name;
            MethodInfo[] methods =
                solution.GetMethods()
                .Where((m) =>
                    {
                        return !ObjectMethods.Contains(m.Name) &&
                        m.IsPublic;
                    }).ToArray();
#if DEBUG
            Console.WriteLine($"There are {methods.Length} public methods");

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(method.Name);
            }
#endif
            if (methods.Length == 0) 
                throw new Exception("There are 0 public methods in this class");
            //获取该方法的参数集
            Type[] ParmTypeSet =
                (from p in methods[0].GetParameters()
                select p.ParameterType).ToArray();

#if DEBUG
            Console.WriteLine($"There are {ParmTypeSet.Length} parameters");

            foreach (Type t in ParmTypeSet)
            {
                Console.WriteLine(t.Name);
            }
#endif

        }
    }
}
