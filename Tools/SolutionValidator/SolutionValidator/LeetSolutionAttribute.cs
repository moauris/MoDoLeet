using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionValidator
{
    [AttributeUsage(AttributeTargets.Class
        , Inherited = false, AllowMultiple = true)]
    public sealed class LeetSolutionAttribute : Attribute
    {
        
    }
    [AttributeUsage(AttributeTargets.Method
    , Inherited = false, AllowMultiple = true)]
    public sealed class LeetMethodAttribute : Attribute
    {

    }
}
