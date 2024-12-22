public class Solution
{
    public string RemoveOuterParentheses(string s)
    {
        StringBuilder sb = new StringBuilder();
        int open = 0;
        int i = 0;
        while (i < s.Length)
        {
            if (s[i] == '(')
            {
                if (open > 0)
                {
                    sb.Append(s[i]);
                }
                open++;
            }
            else
            {
                open--;
                if (open > 0)
                {
                    sb.Append(s[i]);
                }
            }
            i++;
        }
        return sb.ToString();
    }
}