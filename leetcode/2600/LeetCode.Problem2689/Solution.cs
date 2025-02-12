public class RopeTreeNode
{
    public int len;
    public string val;
    public RopeTreeNode? left;
    public RopeTreeNode? right;
    
    public RopeTreeNode(int len = 0, string val = "", RopeTreeNode? left = null, RopeTreeNode? right = null)
    {
        this.len = len;
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    public char GetKthCharacter(RopeTreeNode root, int k)
    {
        return GetKthCharacterImpl(root, k - 1);
    }

    private char GetKthCharacterImpl(RopeTreeNode root, int k)
    {
        if (root.left == null && root.right == null)
        {
            return root.val[k];
        }
        
        int leftWeight = (root.left == null) 
            ? 0 
            : Math.Max(root.left.len, root.left.val?.Length ?? 0);
        
        if (k < leftWeight)
        {
            return GetKthCharacterImpl(root.left!, k);
        }
        else
        {
            return GetKthCharacterImpl(root.right!, k - leftWeight);
        }
    }
}
