using System.Text;

public class Solution {
    public int LengthOfLastWord(string s) {
        string lastWord = string.Empty;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                if (sb.Length > 0) 
                {
                    lastWord = sb.ToString();
                    sb.Clear();
                }
            }
            else
            {
                sb.Append(s[i]);
            }
        }

        if (sb.Length > 0) 
        {
            lastWord = sb.ToString();
        }

        return lastWord.Length;
    }
}