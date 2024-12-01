public class Solution
{
    public bool IsPalindrome(string s)
    {
        if (s.Length < 2)
        {
            return true;
        }

        int i = 0, j = s.Length - 1;
        while (i < j) {
            while (i < j && !char.IsLetterOrDigit(s[i])) {
                i++;
            }

            while (i < j && !char.IsLetterOrDigit(s[j])) {
                j--;
            }

            if (i > j) {
                break;
            }

            if (char.ToLower(s[i]) != char.ToLower(s[j])) {
                return false;
            }
            i++;
            j--;
        }

        return true;
    }
}