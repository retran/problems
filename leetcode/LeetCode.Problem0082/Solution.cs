public class Solution
{
    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null)
        {
            return head;
        }

        var current = head;
        ListNode prev = null;
        while (current != null && current.next != null)
        {
            if (current.val == current.next.val)
            {
                while (current.next != null && current.val == current.next.val)
                {

                    current.next = current.next.next;
                }

                if (prev == null)
                {
                    head = current.next;
                    current = head;
                }
                else
                {
                    prev.next = current.next;
                    current = prev.next;
                }
            }
            else
            {
                prev = current;
                current = current.next;
            }
        }

        return head;
    }
}