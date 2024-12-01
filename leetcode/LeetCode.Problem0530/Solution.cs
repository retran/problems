
public class Solution
{
    public int GetMinimumDifference(TreeNode root)
    {
        var stack = new Stack<TreeNode>();
        var current = root;
        int? prev = null;
        var min = int.MaxValue;
        stack.Push(current);

        while (stack.Count > 0)
        {
            if (current != null) 
            {
                current = current.left;
            }
            
            if (current != null)
            {
                stack.Push(current);
            }
            else
            {
                current = stack.Pop();
                if (prev != null)
                {
                    min = Math.Min(min, Math.Abs(current.val - prev.Value));
                }
                prev = current.val;
                current = current.right;
                if (current != null)
                {
                    stack.Push(current);
                }
            }
        }

        return min;
    }
}