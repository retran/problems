public class Solution
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (!set.Add(nums[i]))
            {
                return true;
            }
            if (i >= k)
            {
                set.Remove(nums[i - k]);
            }
        }
        return false;
    }
}