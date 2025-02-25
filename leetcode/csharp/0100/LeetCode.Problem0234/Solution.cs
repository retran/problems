public class Solution
{
    public bool IsPalindrome(ListNode head)
    {
        if (head == null)
        {
            return true;
        }

        var slow = head;
        var fast = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        var prev = (ListNode)null;
        var current = slow;
        while (current != null)
        {
            var next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }

        var left = head;
        var right = prev;
        while (right != null)
        {
            if (left.val != right.val)
            {
                return false;
            }
            left = left.next;
            right = right.next;
        }

        return true;
    }
}