public class Solution
{
    public int MaxLevelSum(TreeNode root)
    {
        int maxLevelSum = int.MinValue;
        int maxLevelIndex = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int currentLevelIndex = 0;
        while (queue.Count > 0)
        {
            currentLevelIndex++;
            int currentLevelSize = queue.Count;
            int currentLevelSum = 0;
            while (currentLevelSize > 0)
            {
                var node = queue.Dequeue();
                currentLevelSum += node.val;

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }

                currentLevelSize--;
            }

            if (maxLevelSum < currentLevelSum)
            {
                maxLevelSum = currentLevelSum;
                maxLevelIndex = currentLevelIndex;
            }
        }
        return maxLevelIndex;
    }
}