public class Solution
{
    public Node LowestCommonAncestor(Node p, Node q)
    {
        var pstack = new Stack<Node>();
        var qstack = new Stack<Node>();

        var currentp = p;
        var currentq = q;

        while (currentp != null)
        {
            pstack.Push(currentp);
            currentp = currentp.parent;
        }

        while (currentq != null)
        {
            qstack.Push(currentq);
            currentq = currentq.parent;
        }

        Node lca = null;

        while (pstack.Count > 0 && qstack.Count > 0 && pstack.Peek() == qstack.Peek())
        {
            lca = pstack.Pop();
            qstack.Pop();
        }

        return lca;
    }
}