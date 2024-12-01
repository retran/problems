public class Solution
{
    public string ReverseVowels(string s)
    {
        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        var chars = s.ToCharArray();
        int i = 0, j = s.Length - 1;
        while (i < j)
        {
            if (!vowels.Contains(chars[i]))
            {
                i++;
                continue;
            }

            if (!vowels.Contains(chars[j]))
            {
                j--;
                continue;
            }

            var temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
            i++;
            j--;
        }
        return new string(chars);
    }
}