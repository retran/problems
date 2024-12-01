public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        var dummy = new ListNode();

        ListNode current = dummy;

        while (true)
        {
            int minIndex = -1;
            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i] == null)
                {
                    continue;
                }

                if (minIndex == -1 || lists[i].val < lists[minIndex].val)
                {
                    minIndex = i;
                }
            }

            if (minIndex == -1)
            {
                break;
            }

            current.next = lists[minIndex];
            current = current.next;
            lists[minIndex] = lists[minIndex].next;
        }

        return dummy.next;
    }
}