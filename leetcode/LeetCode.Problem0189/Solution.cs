public class Solution {
    public void Rotate(int[] nums, int k) {
        k = k % nums.Length;

        if (k == 0) {
            return;
        }

        int[] tmp = new int[k];
        for (int i = 0; i < k; i++) {
            tmp[i] = nums[nums.Length - k + i];
        }

        for (int i = nums.Length - 1; i > k - 1; i--)
        {
            nums[i] = nums[i - k];
        }

        for (int i = 0; i < k; i++) {
            nums[i] = tmp[i];
        }
    }
}