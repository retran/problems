public class Solution
{
    public ListNode DeleteMiddle(ListNode head)
    {
        if (head?.next == null)
        {
            return null;
        }

        ListNode slow = head;
        ListNode fast = head;
        ListNode prev = head;

        while (fast?.next != null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next?.next;
        }

        var next = slow.next;
        prev.next = next;
        slow.next = null;

        return head;
    }
}