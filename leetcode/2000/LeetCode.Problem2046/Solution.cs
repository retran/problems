public class Solution
{
    public ListNode SortLinkedList(ListNode head)
    {
        ListNode lastLeft = null;
        ListNode firstRight = null;

        var current = head;
        var currentLeft = lastLeft;
        var currentRight = firstRight;

        while (current != null)
        {
            if (current.val < 0)
            {
                var node = new ListNode(current.val, currentLeft);
                if (lastLeft == null)
                {
                    lastLeft = node;
                }
                currentLeft = node;
            }
            else
            {
                var node = new ListNode(current.val);
                if (firstRight == null)
                {
                    firstRight = node;
                    currentRight = node;
                }
                else
                {
                    currentRight.next = node;
                    currentRight = node;
                }
            }
            current = current.next;
        }

        if (lastLeft == null)
        {
            return firstRight;
        }

        if (firstRight == null)
        {
            return currentLeft;
        }

        lastLeft.next = firstRight;

        return currentLeft;
    }
}