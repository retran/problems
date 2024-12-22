
public class Solution
{
    public bool HasCycle(ListNode head)
    {
        var fast = head?.next?.next;
        var slow = head;

        while (fast != slow && fast != null && slow != null)
        {
            fast = fast?.next?.next;
            slow = slow?.next;
        }

        return fast != null && slow != null;;
    }
}