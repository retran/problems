public class Solution {
    public int RemoveDuplicates(int[] nums) {
        int i = 0;
        int j = 0;
        while (j < nums.Length) {
            if (i > 0 && nums[j] == nums[i - 1]) {
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