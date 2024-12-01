class Solution {
    public String simplifyPath(String path) {
        String[] parts = path.split("/");

        Stack<String> stack = new Stack<>();

        for (int i = 0; i < parts.length; i++) {
            if (parts[i].isEmpty()) {
                continue;
            }

            if (parts[i].equals(".")) {
                continue;
            }

            if (parts[i].equals("..")) {
                if (!stack.isEmpty()) {
                    stack.pop();
                }
                continue;
            }

            stack.push(parts[i]);
        }

        return "/" + String.join("/", stack);
    }
}