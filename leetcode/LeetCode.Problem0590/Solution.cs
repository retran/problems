public class Solution
{
    private void Postorder(Node root, IList<int> traversal)
    {
        if (root == null)
        {
            return;
        }

        if (root.children != null)
        {
            foreach (var child in root.children)
            {
                Postorder(child, traversal);
            }
        }

        traversal.Add(root.val);        
    }

    public IList<int> Postorder(Node root)
    {
        var traversal = new List<int>();
        Postorder(root, traversal);
        return traversal;
    }
}