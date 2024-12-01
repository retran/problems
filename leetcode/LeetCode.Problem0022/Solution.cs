public class Solution
{
    private void Backtrack(IList<string> result, StringBuilder current, int open, int close, int max)
    {
        if (current.Length == max * 2)
        {
            result.Add(current.ToString());
            return;
        }

        if (open < max)
        {
            current.Append('(');
            Backtrack(result, current, open + 1, close, max);
            current.Remove(current.Length - 1, 1);
        }

        if (close < open)
        {
            current.Append(')');
            Backtrack(result, current, open, close + 1, max);
            current.Remove(current.Length - 1, 1);
        }
    }

    public IList<string> GenerateParenthesis(int n)
    {
        var result = new List<string>();
        Backtrack(result, new StringBuilder(), 0, 0, n);
        return result;
    }
}