public class Solution
{
    public Node Insert(Node head, int insertVal)
    {
        var node = new Node(insertVal);

        if (head == null)
        {
            node.next = node;
            return node;
        }

        var current = head;

        while (current.next != head)
        {
            if (current.val <= insertVal && insertVal <= current.next.val)
            {
                break;
            }

            if (current.val > current.next.val)
            {
                if (insertVal >= current.val || insertVal <= current.next.val)
                {
                    break;
                }
            }
            current = current.next;
        }
        node.next = current.next;
        current.next = node;

        return head;
    }
}