public class Solution
{
    public ListNode Reverse(ListNode prev, ListNode next)
    {
        ListNode last = prev.next;
        ListNode current = last.next;
        while (current != next)
        {
            last.next = current.next;
            current.next = prev.next;
            prev.next = current;
            current = last.next;
        }

        return last;
    }

    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null || k == 1)
        {
            return head;
        }

        ListNode dummy = new ListNode(0);
        dummy.next = head;

        ListNode prev = dummy;
        ListNode current = head;
        int count = 0;
        while (current != null)
        {
            count++;
            if (count % k == 0)
            {
                prev = Reverse(prev, current.next);
                current = prev.next;
            }
            else
            {
                current = current.next;
            }
        }

        return dummy.next;
    }
}