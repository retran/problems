public class Solution
{
    private void Preorder(Node root, IList<int> traversal)
    {
        if (root == null)
        {
            return;
        }

        traversal.Add(root.val);

        if (root.children == null)
        {
            return;
        }

        foreach (var child in root.children)
        {
            Preorder(child, traversal);
        }
    }

    public IList<int> Preorder(Node root)
    {
        var traversal = new List<int>();
        Preorder(root, traversal);
        return traversal;
    }
}