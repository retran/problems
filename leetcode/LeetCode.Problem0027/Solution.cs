public class Solution {
    public int RemoveElement(int[] nums, int val) {
        int i = 0; int j = 0;

        while (j < nums.Length) {
            if (nums[j] == val) {
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