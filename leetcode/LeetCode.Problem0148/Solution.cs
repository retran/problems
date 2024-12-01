public class Solution
{
    public ListNode Merge(ListNode left, ListNode right)
    {
        ListNode dummy = new ListNode();
        ListNode current = dummy;
        while (left != null && right != null)
        {
            if (left.val < right.val)
            {
                current.next = left;
                left = left.next;
            }
            else
            {
                current.next = right;
                right = right.next;
            }

            current = current.next;
        }

        if (left != null)
        {
            current.next = left;
        }

        if (right != null)
        {
            current.next = right;
        }

        return dummy.next;
    }

    public ListNode SortList(ListNode head)
    {
        if (head == null)
        {
            return null;
        }

        if (head.next == null)
        {
            return head;
        }

        // find median
        ListNode slow = head;
        ListNode fast = head;
        ListNode prev = null;
        while (fast != null && fast.next != null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        prev.next = null;

        ListNode left = SortList(head);
        ListNode right = SortList(slow);

        return Merge(left, right);
    }
}