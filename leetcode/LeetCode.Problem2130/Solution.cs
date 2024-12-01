public class Solution
{
    public int PairSum(ListNode head)
    {
        var slow = head;
        var fast = head;
        while (fast?.next != null)
        {
            slow = slow.next;
            fast = fast.next?.next;
        }

        var current = slow;
        ListNode reversedListCurrent = null;

        int count = 0;
        while (current != null)
        {
            count++;
            var newNode = new ListNode();
            newNode.val = current.val;
            newNode.next = reversedListCurrent;

            reversedListCurrent = newNode;
            current = current.next;
        }

        int i = 0;
        current = head;
        int maxTwinSum = int.MinValue;
        while (i < count)
        {
            maxTwinSum = Math.Max(current.val + reversedListCurrent.val, maxTwinSum);
            i++;
            current = current.next;
            reversedListCurrent = reversedListCurrent.next;
        }

        return maxTwinSum;
    }
}