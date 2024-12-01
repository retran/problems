public class Solution {
    private Node head;
    private Node prev;

    public Node TreeToDoublyList(Node root) {
        if (root == null) {
            return null;
        }

        // Reset head and prev for each call
        head = null;
        prev = null;

        // Perform in-order traversal
        InOrderTraversal(root);

        // Make it circular
        head.left = prev;
        prev.right = head;

        return head;
    }

    private void InOrderTraversal(Node node) {
        if (node == null) {
            return;
        }

        // Traverse the left subtree
        InOrderTraversal(node.left);

        // Process the current node
        if (prev == null) {
            // First node visited, it will be the head of the list
            head = node;
        } else {
            // Link the previous node and the current node
            prev.right = node;
            node.left = prev;
        }

        // Move prev to the current node
        prev = node;

        // Traverse the right subtree
        InOrderTraversal(node.right);
    }
}
