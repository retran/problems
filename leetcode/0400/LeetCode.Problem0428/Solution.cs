public class Codec {
    // Encodes a tree to a single string.
    public string serialize(Node root) {
        if (root == null) return "";

        StringBuilder sb = new StringBuilder();
        // First append the value of the node
        sb.Append(root.val).Append(",").Append(root.children.Count).Append(";");

        // Then serialize each child
        foreach (var child in root.children) {
            sb.Append(serialize(child));
        }

        return sb.ToString();
    }

    // Decodes your encoded data to tree.
    public Node deserialize(string data) {
        if (string.IsNullOrEmpty(data)) return null;

        Queue<string> queue = new Queue<string>(data.Split(';', StringSplitOptions.RemoveEmptyEntries));
        return DeserializeHelper(queue);
    }

    private Node DeserializeHelper(Queue<string> queue) {
        // Get the first element from the queue
        var first = queue.Dequeue();
        var parts = first.Split(',', StringSplitOptions.RemoveEmptyEntries);
        
        int val = int.Parse(parts[0]);
        int childrenCount = int.Parse(parts[1]);
        
        Node node = new Node(val, new List<Node>());

        // Deserialize each child based on the number of children
        for (int i = 0; i < childrenCount; i++) {
            node.children.Add(DeserializeHelper(queue));
        }

        return node;
    }
}