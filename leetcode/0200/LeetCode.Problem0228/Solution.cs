public class Solution {
    public IList<string> SummaryRanges(int[] nums) 
    {
        var result = new List<string>();
        int left = 0;
        int right = 0;

        while (right < nums.Length) 
        {
            if (right + 1 < nums.Length && nums[right + 1] == nums[right] + 1) 
            {
                right++;
            } 
            else 
            {
                if (left == right) 
                {
                    result.Add(nums[left].ToString());
                } 
                else 
                {
                    result.Add(nums[left].ToString() + "->" + nums[right].ToString());
                }
                right++;
                left = right;
            }
        }

        return result;
    }
}