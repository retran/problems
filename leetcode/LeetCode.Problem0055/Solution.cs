public class Solution {
    public bool CanJump(int[] nums) {
        int maxJump = 0;
        for (int i = 0; i < nums.Length; i++) {
            if (i > maxJump) {
                return false;
            }

            int newMaxJump = i + nums[i];
            if (newMaxJump > maxJump) {
                maxJump = newMaxJump;
            }

            if (maxJump >= nums.Length - 1) {
                return true;
            }
        }

        return true;
    }
}