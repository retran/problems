public class Solution
{
    private void ReverseWord(char[] chars, int i, int j)
    {
        while (i < j)
        {
            char tmp = chars[i];
            chars[i] = chars[j];
            chars[j] = tmp;
            i++;
            j--;
        }
    }

    public string ReverseWords(string s)
    {
        char[] chars = s.ToCharArray();

        int start = 0;
        int end = 0;

        while (start < chars.Length)
        {
            while (start < s.Length && char.IsWhiteSpace(chars[start]))
            {
                start++;
            }

            end = start;

            while (end < s.Length && !char.IsWhiteSpace(chars[end]))
            {
                end++;
            }

            ReverseWord(chars, start, end - 1);

            start = end;
        }

        return new string(chars);
    }
}