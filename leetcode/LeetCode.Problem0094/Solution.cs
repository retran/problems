public class Solution {
    public void Traverse(TreeNode root, IList<int> result)
    {
        if (root == null)
        {
            return;
        }
        
        Traverse(root.left, result);
        result.Add(root.val);
        Traverse(root.right, result);
    }

    public IList<int> InorderTraversal(TreeNode root) {
        var result = new List<int>();
        Traverse(root, result);
        return result;
    }
}