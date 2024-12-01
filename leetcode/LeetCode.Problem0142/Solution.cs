public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null)
        {
            return head;
        }

        var slow = head;
        var fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
            if (slow == fast)
            {
                break;
            }
        }

        if (fast == null || fast.next == null)
        {
            return null;
        }

        slow = head;
        while (slow != fast)
        {
            slow = slow.next;
            fast = fast.next;
        }

        return slow;
    }
}