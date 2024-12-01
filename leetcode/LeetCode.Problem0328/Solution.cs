public class Solution
{
    public ListNode OddEvenList(ListNode head)
    {
        var leftDummy = new ListNode();
        var rightDummy = new ListNode();

        var current = head;
        var leftCurrent = leftDummy;
        var rightCurrent = rightDummy;

        int index = 1;
        while (current != null)
        {
            if (index % 2 == 0)
            {
                rightCurrent.next = current;
                rightCurrent = current;
            }
            else
            {
                leftCurrent.next = current;
                leftCurrent = current;
            }
            current = current.next;
            index++;
        }

        leftCurrent.next = rightDummy.next;
        rightCurrent.next = null;

        return leftDummy.next;
    }
}