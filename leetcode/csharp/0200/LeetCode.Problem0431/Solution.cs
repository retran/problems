public class Codec {
    // Encodes an n-ary tree to a binary tree.
    public TreeNode encode(Node root) {
        if (root == null) return null;

        TreeNode binaryNode = new TreeNode(root.val);

        // The first child becomes the left child.
        if (root.children != null && root.children.Count > 0) {
            binaryNode.left = encode(root.children[0]);
        }

        // The rest of the children become the right siblings.
        TreeNode current = binaryNode.left;
        for (int i = 1; i < root.children.Count; i++) {
            current.right = encode(root.children[i]);
            current = current.right;
        }

        return binaryNode;
    }
    
    // Decodes your binary tree to an n-ary tree.
    public Node decode(TreeNode root) {
        if (root == null) return null;

        Node naryNode = new Node(root.val, new List<Node>());
        
        TreeNode current = root.left;
        while (current != null) {
            naryNode.children.Add(decode(current));
            current = current.right; // Move to the next sibling.
        }

        return naryNode;
    }
}