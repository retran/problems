public class Solution
{
    public Node CopyRandomList(Node head)
    {
        if (head == null) 
        {
            return null;
        }

        Dictionary<Node, Node> map = new Dictionary<Node, Node>();

        Node newHead = new Node(head.val);
        map.Add(head, newHead);

        Node current = head;
        Node newCurrent = newHead;
        while (current != null)
        {
            if (current.next != null)
            {
                if (!map.ContainsKey(current.next))
                {
                    map.Add(current.next, new Node(current.next.val));
                }
                newCurrent.next = map[current.next];
            }

            if (current.random != null)
            {
                if (!map.ContainsKey(current.random))
                {
                    map.Add(current.random, new Node(current.random.val));
                }
                newCurrent.random = map[current.random];
            }

            current = current.next;
            newCurrent = newCurrent.next;
        }

        return newHead;
    }
}