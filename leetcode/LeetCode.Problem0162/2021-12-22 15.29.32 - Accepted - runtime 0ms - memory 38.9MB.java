class Solution {
    public int findPeakElement(int[] nums) {
        int left = 0;
        int right = nums.length - 1;
        while (right - left > 1) {
            int mid = (right + left) / 2;
            if (mid != nums.length - 1 && nums[mid] < nums[mid + 1]) {
                left = mid;
            } else if (mid != 0 && nums[mid] < nums[mid - 1]) {
                right = mid;
            } else {
                return mid;
            }
        }
        
        return nums[left] > nums[right] ? left : right;
    }
}