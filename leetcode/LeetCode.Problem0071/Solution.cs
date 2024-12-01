public class Solution
{
    public string SimplifyPath(string path)
    {
        string[] parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        Stack<string> stack = new Stack<string>();

        foreach (var part in parts) {
            if (part == "..") {
                if (stack.Count > 0) {
                    stack.Pop();
                }
            } else if (part != ".") {
                stack.Push(part);
            }
        }

        return "/" + string.Join("/", stack.Reverse());
    }
}