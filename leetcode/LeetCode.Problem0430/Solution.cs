public class Solution
{
    public Node Flatten(Node head)
    {
        Stack<Node> stack = new Stack<Node>();
        var current = head;

        while (current != null)
        {
            if (current.child != null)
            {
                if (current.next != null)
                {
                    stack.Push(current.next);
                }
                current.next = current.child;
                current.child.prev = current;
                current.child = null;
            }

            if (current.next == null && stack.Count > 0)
            {
                current.next = stack.Pop();
                current.next.prev = current;
            }
            current = current.next;
        }

        return head;
    }
}