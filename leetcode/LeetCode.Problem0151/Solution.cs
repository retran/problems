public class Solution
{
    public void ReverseInplace(char[] chars, int from, int to) 
    {
        while (from < to)
        {
            char temp = chars[from];
            chars[from] = chars[to];
            chars[to] = temp;
            from++;
            to--;
        }
    }

    public string ReverseWords(string s)
    {
        char[] chars = s.ToCharArray();

        int i = 0;
        int j = s.Length - 1;
        while (true)
        {
            if (i >= j)
            {
                break;
            }

            char temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
            i++;
            j--;
        }

        i = 0;
        j = 0;
        bool skipNextSpaces = true; // to skip leading spaces
        while (j < s.Length)
        {
            if (chars[j] == ' ')
            {
                if (skipNextSpaces)
                {
                    j++;
                    continue;
                }
                else
                {
                    chars[i] = chars[j];
                    i++;
                    j++;
                    skipNextSpaces = true;
                    continue;
                }
            }

            chars[i] = chars[j];
            i++;
            j++;
            skipNextSpaces = false;
        }

        while (i < s.Length)
        {
            chars[i] = ' ';
            i++;
        }

        int start = 0;
        int end = 0;

        while (start < s.Length)
        {
            while (start < s.Length && chars[start] == ' ')
            {
                start++;
            }

            if (start == s.Length)
            {
                break;
            }

            end = start + 1;
            while (end < s.Length && chars[end] != ' ')
            {
                end++;
            }

            ReverseInplace(chars, start, end - 1);
            start = end;
        }


        return new string(chars).Trim();
    }
}