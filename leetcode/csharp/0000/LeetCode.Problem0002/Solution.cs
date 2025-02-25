public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var dummy = new ListNode();
        var current = dummy;
        int carry = 0;

        while (l1 != null || l2 != null)
        {
            int sum = carry;

            if (l1 != null)
            {
                sum += l1.val;
                l1 = l1.next;
            }

            if (l2 != null)
            {
                sum += l2.val;
                l2 = l2.next;
            }

            carry = sum / 10;
            sum %= 10;

            current.next = new ListNode(sum);
            current = current.next;
        }

        if (carry > 0)
        {
            current.next = new ListNode(carry);
        }

        return dummy.next;
    }
}