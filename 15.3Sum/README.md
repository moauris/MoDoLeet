# Three Sum (#15)

https://leetcode.com/problems/3sum/

Three sum is a classic question that looks deceptively simple.

I managed to get to 300/315 test items passed, but in bulk and large numbers iteration throw all possibilities is just unrealistic. It ran through a **full 6 minutes!**

Before I give up and find another answer, I need to rethink how to do it without busting the runtime limit.

So far I have done the below:

1. Sort the list from min to max
2. Eliminate more than 3 times repeated elements
3. Three pointer iteration through all elements

However, in the test example there is one that contains 3k elements, and going through all simply takes too much time.

## Clever Solution

The solution with highest votes is called a "Concise O(N^2)".

```java
public List<List<Integer>> threeSum(int[] num) {
    Arrays.sort(num);
    List<List<Integer>> res = new LinkedList<>(); 
    for (int i = 0; i < num.length-2; i++) {
        if (i == 0 || (i > 0 && num[i] != num[i-1])) {
            int lo = i+1, hi = num.length-1, sum = 0 - num[i];
            while (lo < hi) {
                if (num[lo] + num[hi] == sum) {
                    res.add(Arrays.asList(num[i], num[lo], num[hi]));
                    while (lo < hi && num[lo] == num[lo+1]) lo++;
                    while (lo < hi && num[hi] == num[hi-1]) hi--;
                    lo++; hi--;
                } else if (num[lo] + num[hi] < sum) lo++;
                else hi--;
           }
        }
    }
    return res;
}
```

Sort the list, the same.

create empty result object, same.

Enters the iteration.

Ah, I see. To speed up the runtime, no three pointer is needed. For three pointers to work we get a combination of 3000+ objects, and there is no way for that to iterate through all these.

and it actually implements a 2 pointer, with one pointer running from min to max, and the other from max to min.

### Difficult Point #1: Initial iteration condition

The condition used is when 

1. when iterator was 0
2. or, if iterator is greater than 0 and current item is not equal to previous item

### Difficult Point #2: Entering 2 pointer iteration

There are two integer variables used to represent high and low pointers. Low starts with 1 position right of iterator, and high starts with the last number.

Also there is a sum variable that is represented by the negative of current item.

Limitation is when low number is less than high number, which is when two pointers meet in the middle. Iterate pointers when it is true. When they meet, increment low for 1, and start the iteration again.

skip 1 digit of low and high iterations, if the previous is the same as the current.

### Difficult Point #3: When to increment low pointer and when high pointer

If the sum of low number and high number is less than the negative of current item, increase low.

If the sum of low number and high number is greater than the negative of current item, decrease high.

### Difficult Point #4: Why is double judging low less than high necessary

In line 10 and 11 of the clever code, there are:

```Java
                    while (lo < hi && num[lo] == num[lo+1]) lo++;
                    while (lo < hi && num[hi] == num[hi-1]) hi--;
```

But the entire iteration is judged based on `lo < hi` already, is this necessary?

Yes, because lo and high kept on changing during the process.

Each time there need to be a re-judgement.

## Implement by myself

Original Implementation:

```C#
    public class Solution
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
#if DEBUG
            Console.Write("Checking Array: [");
            foreach (int item in nums)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("]");
#endif
            IList<IList<int>> result = new List<IList<int>>();
            if (nums.Length < 3) return result;
            //超时问题：简化初始数组
            //每个相同元素最大保留2个
            //不需要遍历时查重

            Array.Sort<int>(nums);
            List<int> TwoDumMax = new List<int>();
            TwoDumMax.Add(nums[0]);
            TwoDumMax.Add(nums[1]);
            TwoDumMax.Add(nums[2]);
            int n = nums.Length;
            for (int i = 3; i < n; i++)
            {
                if (!(nums[i] == nums[i-1] && nums[i - 1] == nums[i-2] && nums[i - 2] == nums[i - 3]))
                {
                    TwoDumMax.Add(nums[i]);
                }
            }
            nums = TwoDumMax.ToArray();
            n = nums.Length;
#if DEBUG
            Console.Write("Sorted Array: [");
            foreach (int item in nums)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("]");
            Console.WriteLine("Current Array contains {0} elements", n);
            Console.ReadLine();
#endif
            //Dictionary<int[], int> dupcheck;
            //判定：如果length<3直接返回null

            //遍历集合中的每一个元素
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 1; j < n - 1; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        List<int> SingleCollection = new List<int>
                        {
                            nums[i], nums[j], nums[k]
                        };
                        if (IsSumToZero(SingleCollection))
                        {
                            //此处需要查重
#if DEBUG
                            Console.Write("Found!, i=");
                            Console.Write(i + ",");
                            Console.Write("j=");
                            Console.Write(j + ",");
                            Console.Write("k=");
                            Console.WriteLine(k);

#endif
                            /*
                            bool hasDup = false;
                            foreach (List<int> item in result)
                            {
                                if (item[0] == SingleCollection[0] 
                                    && item[2] == SingleCollection[2])
                                {
                                    hasDup = true;
                                }
                            }
                            */
                            //if(!hasDup) result.Add(SingleCollection);
                            result.Add(SingleCollection);
                        }
                    }
                }
            }
#if DEBUG
            foreach (List<int> item in result)
            {
                foreach (int i in item)
                {
                    Console.Write(i + ",");
                }
                Console.WriteLine();
            }
#endif


            return result;
        }

        private bool IsSumToZero(List<int> singleCollection)
        {
            int Sum = 0;
            foreach (int item in singleCollection)
            {
                Sum += item;
            }
            return Sum == 0;
        }
    }
```

Problems:

1. made a iteration of all elements at first, with the increasing number of collections, the runtime become unmanageable, even with duplications removed.

Re-Implementation:

```C#
    public class Solution
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            //双指针寻找
            Array.Sort<int>(nums);
            IList<IList<int>> res = new List<IList<int>>();
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i == 0 || (i > 0 && nums[i] != nums[i-1]))
                {
                    int lo = i + 1, hi = nums.Length - 1, sum = 0 - nums[i];
                    while (lo < hi)
                    {
                        if (nums[lo] + nums[hi] == sum)
                        {
                            res.Add(new List<int> { nums[i], nums[lo], nums[hi] });
                            while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
                            while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
                            lo++; hi--;
                        }
                        else if (nums[lo] + nums[hi] < sum) lo++;
                        else hi--;
                    }
                }
            }
            return res;

        }
    }
```

