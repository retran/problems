public class Solution
{
    public ListNode SwapPairs(ListNode head)
    {
        var dummyHead = new ListNode(0, head);
        var current = dummyHead;

        while (current != null && current.next != null && current.next.next != null)
        {
            var next = current.next;
            var nextnext = current.next.next;

            next.next = nextnext.next;
            nextnext.next = next;
            current.next = nextnext;

            current = next;
        }

        return dummyHead.next;
    }
}