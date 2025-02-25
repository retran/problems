public class Solution
{
    public ListNode DeleteDuplicatesUnsorted(ListNode head)
    {
        var frequencies = new Dictionary<int, int>();

        var current = head;
        while (current != null)
        {
            if (!frequencies.TryGetValue(current.val, out var count))
            {
                count = 0;
            }
            frequencies[current.val] = count + 1;
            current = current.next;
        }

        var dummy = new ListNode();
        dummy.next = head;

        current = dummy;
        while (current.next != null)
        {
            if (frequencies[current.next.val] > 1)
            {
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }

        return dummy.next;
    }
}