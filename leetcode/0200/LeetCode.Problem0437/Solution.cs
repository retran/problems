public class Solution
{
    private Dictionary<long, int> _prefixSums = new Dictionary<long, int>();

    public int PathSum(TreeNode root, long currentSum, int targetSum)
    {
        int count = 0;
        if (root == null)
        {
            return 0;
        }

        currentSum += root.val;
        if (currentSum == targetSum)
        {
            count++;
        }

        if (_prefixSums.TryGetValue(currentSum - targetSum, out var prefixCount))
        {
            count += prefixCount;
        }

        if (!_prefixSums.TryGetValue(currentSum, out var value))
        {
            value = 0;
        }

        _prefixSums[currentSum] = value + 1;

        count += PathSum(root.left, currentSum, targetSum);
        count += PathSum(root.right, currentSum, targetSum);

        _prefixSums[currentSum]--;

        return count;
    }


    public int PathSum(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            return 0;
        }

        return PathSum(root, 0, targetSum);
   }
}