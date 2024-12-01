/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode InsertionSortList(ListNode head) {
        // If the list is empty or has only one node, it is already sorted
        if (head == null || head.next == null) return head;

        // Create a dummy node to help with the insertion process
        ListNode dummy = new ListNode(0);
        ListNode current = head;

        while (current != null) {
            // At each iteration, we will insert `current` into the sorted part
            ListNode prev = dummy; // Start from the dummy node
            ListNode next = current.next; // Keep the next node to process later

            // Find the right position to insert the current node
            while (prev.next != null && prev.next.val < current.val) {
                prev = prev.next; // Move to the right in the sorted part
            }

            // Insert the current node in the sorted part
            current.next = prev.next; // Point current to the next node in sorted part
            prev.next = current; // Insert current in the sorted part

            // Move to the next node in the original list
            current = next;
        }

        // Return the head of the sorted list, which is after the dummy node
        return dummy.next;
    }
}
