public class Solution
{
    public bool CanConstruct(string ransomNote, string magazine)
    {
        if (ransomNote.Length > magazine.Length)
        {
            return false;
        }

        var magazineChars = new int[26];
        foreach (var c in magazine)
        {
            magazineChars[c - 'a']++;
        }

        foreach (var c in ransomNote)
        {
            if (magazineChars[c - 'a'] == 0)
            {
                return false;
            }

            magazineChars[c - 'a']--;
        }

        return true;
    }
}