public class Solution {
    public int MajorityElement(int[] nums) {
        var k = nums.Length / 2;
        var counts = new Dictionary<int, int>();

        int i = 0;
        while (i < nums.Length) {
            int count = 0;
            if (counts.TryGetValue(nums[i], out count)) 
            {
                count++;
            }
            else
            {
                count = 1;
            }

            if (count > k) {
                return nums[i];
            }

            counts[nums[i]] = count;
            i++;
        }

        return -1;
    }
}