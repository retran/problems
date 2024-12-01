public class Solution
{
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        var left = head;
        var right = head;

        int i = 0;
        for (i = 0; i < n; i++)
        {
            right = right.next;
            if (right == null)
            {
                if (i == n - 1)
                {
                    head = head.next;
                    return head;
                }
                else if (i < n)
                {
                    return head;
                }
            }
        }

        while (right.next != null) 
        {
            right = right.next;
            left = left.next;
        }

        left.next = left.next.next;

        return head;
    }
}