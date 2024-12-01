public class Solution
{
    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        if (pushed.Length != popped.Length)
        {
            return false;
        }

        var stack = new Stack<int>();
        int i = 0;
        int j = 0;

        while (i < pushed.Length || j < popped.Length)
        {
            if (stack.Count == 0)
            {
                if (i == popped.Length)
                {
                    return false;
                }
                stack.Push(pushed[i]);
                i++;
                continue;
            }

            if (j == pushed.Length)
            {
                return false;
            }

            if (stack.Peek() == popped[j])
            {
                stack.Pop();
                j++;
                continue;
            }

            if (i == popped.Length)
            {
                return false;
            }

            stack.Push(pushed[i]);
            i++;
        }

        return true;
    }
}