/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public TreeNode ToTree(int[] nums, int from, int to)
    {
        int median = (from + to) / 2;
        var node = new TreeNode(nums[median]);
        if (from < median)
        {
            node.left = ToTree(nums, from, median - 1);
        }
        if (median < to)
        {
            node.right = ToTree(nums, median + 1, to);
        }
        return node;
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        return ToTree(nums, 0, nums.Length - 1);
    }
}