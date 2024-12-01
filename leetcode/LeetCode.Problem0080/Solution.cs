public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if (nums.Length < 3) {
            return nums.Length;
        }

        int i = 2;
        int j = 2;
        while (j < nums.Length) {
            if (nums[j] == nums[i - 2]) {
                j++;
                continue;
            }

            nums[i] = nums[j];
            i++;
            j++;
        }

        return i;
    }
}