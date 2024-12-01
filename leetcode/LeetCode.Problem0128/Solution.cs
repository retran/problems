public class Solution
{
    public int LongestConsecutive(int[] nums)
    {
        var set = new HashSet<int>(nums);
        int max = 0;
        foreach (int num in nums)
        {
            if (!set.Contains(num - 1))
            {
                int currentNum = num;
                int currentStreak = 1;
                while (set.Contains(currentNum + 1))
                {
                    currentNum++;
                    currentStreak++;
                }
                max = Math.Max(max, currentStreak);
            }
        }
        return max;
    }
}