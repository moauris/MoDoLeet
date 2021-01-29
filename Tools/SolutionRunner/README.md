# Solution Runner

Runs solutions against parameters and results stored in .txt file

## Usage & Syntax

Testing against a solution.

```c#
using SolutionRunner.Components;

class Program
{
    public static void Main()
    {
        Runner runner = new Runner(Solution_0383);
        runner.LoadCaseFromFile(".\testcase_0383.txt");
    }
}
    
    
```

File: testcase_0383.txt

```
"a"
"b"
false
"aa"
"ab"
false
"aa"
"aab"
true
```

After the runner ran, the console will be used to display info on the execution results like LeetCode.com

## Mechanics

The Runner class should contain members to hold parameter sets which fits the parameter sets and answer types of the target problem and solution. This can be done using reflection.

After the parts are ready, load test cases from file, and generate sets of parameters from the file. If the quantity doesn't match, throw exception.

Then, use these parameters to run the Solution methods, while recording metadata such as run time for each case, etc. In this step, a collection of results are generated.

Last Step, export the results on the console.

