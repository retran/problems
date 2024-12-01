public class Solution
{
    public int MaxVowels(string s, int k)
    {
        if (s.Length < k)
        {
            return 0;
        }

        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
        int count = 0;

        for (int i = 0; i < k; i++)
        {
            if (vowels.Contains(s[i]))
            {
                count++;
            }
        }

        int maxCount = count;

        for (int i = k; i < s.Length; i++)
        {
            if (vowels.Contains(s[i - k]))
            {
                count--;
            }

            if (vowels.Contains(s[i]))
            {
                count++;
            }

            if (maxCount < count)
            {
                maxCount = count;
            }
        }

        return maxCount;
    }
}