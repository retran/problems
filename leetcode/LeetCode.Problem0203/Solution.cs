public class Solution
{
    public ListNode RemoveElements(ListNode head, int val)
    {
        var dummy = new ListNode(0, head);

        var current = dummy;

        while (current.next != null)
        {
            if (current.next.val == val)
            {
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }

        return dummy.next;
    }
}