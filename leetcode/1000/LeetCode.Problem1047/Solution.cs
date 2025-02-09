public class Solution
{
    public string RemoveDuplicates(string s)
    {
        var stack = new Stack<char>();

        foreach (var c in s)
        {
            if (stack.Count == 0 || stack.Peek() != c)
            {
                stack.Push(c);
            } 
            else 
            {
                stack.Pop();
            }
        }

        return new string(stack.Reverse().ToArray());
    }
}