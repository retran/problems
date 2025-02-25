public class Solution
{
    public int MinimumDifference(int[] nums)
    {
        int n = nums.Length;
        int half = n / 2;

        long totalSum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
        }

        var leftSums = GetSubsetSums(nums, 0, half);
        var rightSums = GetSubsetSums(nums, half, n);

        for (int i = 0; i < rightSums.Length; i++)
        {
            rightSums[i].Sort();
        }

        long bestDiff = long.MaxValue;

        for (int leftCount = 0; leftCount <= half; leftCount++)
        {
            int rightCount = half - leftCount;
            var rightList = rightSums[rightCount];

            foreach (long leftSum in leftSums[leftCount])
            {
                long target = totalSum / 2 - leftSum;
                int idx = rightList.BinarySearch(target);

                if (idx < 0)
                {
                    idx = ~idx;
                }

                if (idx < rightList.Count)
                {
                    long candidate = rightList[idx];
                    long currentSum = leftSum + candidate;
                    bestDiff = Math.Min(bestDiff, Math.Abs(totalSum - 2 * currentSum));
                }
                if (idx - 1 >= 0)
                {
                    long candidate = rightList[idx - 1];
                    long currentSum = leftSum + candidate;
                    bestDiff = Math.Min(bestDiff, Math.Abs(totalSum - 2 * currentSum));
                }
            }
        }
        return (int)bestDiff;
    }

    private List<long>[] GetSubsetSums(int[] nums, int start, int end)
    {
        int count = end - start;
        var subsetSums = new List<long>[count + 1];
        for (int i = 0; i <= count; i++)
        {
            subsetSums[i] = new List<long>();
        }

        var stack = new Stack<(int index, int chosenCount, long currentSum)>();
        stack.Push((start, 0, 0));

        while (stack.Count > 0)
        {
            var (index, chosenCount, currentSum) = stack.Pop();
            if (index == end)
            {
                subsetSums[chosenCount].Add(currentSum);
            }
            else
            {
                stack.Push((index + 1, chosenCount + 1, currentSum + nums[index]));
                stack.Push((index + 1, chosenCount, currentSum));
            }
        }

        return subsetSums;
    }
}
