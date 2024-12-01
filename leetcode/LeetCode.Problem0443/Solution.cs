public class Solution
{
    public int Compress(char[] chars)
    {
        if (chars.Length < 2)
        {
            return chars.Length;
        }

        int i = 0;
        int j = 1;
        int count = 1;

        while (j <= chars.Length)
        {
            if (j < chars.Length && chars[j - 1] == chars[j])
            {
                count++;
            }
            else
            {
                chars[i] = chars[j - 1];
                i++;

                if (count > 1)
                {
                    int n = (int)Math.Log10(count) + 1;
                    i += n;
                    for (int k = 0; k < n; k++)
                    {
                        chars[i - k - 1] = (char)('0' + count % 10);
                        count /= 10;
                    }
                }

                count = 1;
            }
            j++;
        }

        return i;
    }
}