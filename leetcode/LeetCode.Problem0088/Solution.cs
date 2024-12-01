// https://leetcode.com/problems/merge-sorted-array

public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {
        int i = m - 1;
        int j = n - 1;
        int k = n + m - 1;

        while (i > -1 && j > -1) {
            if (nums1[i] > nums2[j]) {
                nums1[k] = nums1[i];
                i--;
            }
            else {
                nums1[k] = nums2[j];
                j--;
            }
            k--;
        }

        // we don't need to copy remains of nums1, copy only num2 if something remains
        while (j > -1) {
            nums1[k] = nums2[j];
            j--;
            k--;
        }
    }
}