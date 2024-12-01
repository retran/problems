public class Solution
{
    public ListNode Partition(ListNode head, int x)
    {
        if (head == null || head.next == null)
        {
            return head;
        }

        ListNode headLeft = null;
        ListNode headRight = null;
        ListNode currentLeft = null;
        ListNode currentRight = null;
        var current = head;
        while (current != null)
        {
            if (current.val < x)
            {
                if (currentLeft == null) 
                {
                    currentLeft = current;
                    headLeft = currentLeft;
                }
                else
                {
                    currentLeft.next = current;
                    currentLeft = currentLeft.next;
                }
            }
            else
            {
                if (currentRight == null) 
                {
                    currentRight = current;
                    headRight = currentRight;
                }
                else
                {
                    currentRight.next = current;
                    currentRight = currentRight.next;
                }
            }

            current = current.next;
        }

        if (headLeft == null)
        {
            return headRight;
        }

        if (headRight == null)
        {
            return headLeft;
        }
        
        currentRight.next = null;
        currentLeft.next = headRight;
        return headLeft;
    }
}