public class Solution
{
    public int SpecialArray(int[] nums)
    {
        Array.Sort(nums);

        int left = 0;
        for (int i = 0; i <= nums.Length; i++) 
        {
            while (left < nums.Length && nums[left] < i) 
            {
                left++;
            }

            if (i == nums.Length - left)
            {
                return i;
            }
        }

        return -1;
    }

    public static void Main() {
        Solution solution = new Solution();
        int[] nums = new int[] {3, 5};
        System.Console.WriteLine(solution.SpecialArray(nums));
    }
}