using System.Text;

public class Solution
{
    public string RemoveKdigits(string num, int k)
    {
        var stack = new Stack<char>();

        foreach (char digit in num)
        {
            while (stack.Count > 0 && k > 0 && stack.Peek() > digit)
            {
                stack.Pop();
                k--;
            }
            stack.Push(digit);
        }

        while (k > 0 && stack.Count > 0)
        {
            stack.Pop();
            k--;
        }

        var sb = new StringBuilder();
        bool leadingZero = true;
        foreach (var digit in stack.Reverse())
        {
            if (leadingZero && digit == '0') continue;
            leadingZero = false;
            sb.Append(digit);
        }

        var result = sb.ToString();
        return string.IsNullOrEmpty(result) ? "0" : result;
    }
}
