// https://leetcode.com/problems/minimum-window-substring

public class Solution
{    
    public string MinWindow(string s, string t)
    {
        if (t.Length > s.Length) 
        {
            return string.Empty;
        }

        var templateFrequencyMap = new Dictionary<char, int>();
        var currentFrequencyMap = new Dictionary<char, int>();
        foreach (var c in t)
        {
            if (templateFrequencyMap.ContainsKey(c))
            {
                templateFrequencyMap[c]++;
                currentFrequencyMap[c]++;
            }
            else
            {
                templateFrequencyMap[c] = 1;
                currentFrequencyMap[c] = 1;
            }
        }
        
        int i = 0;
        int j = -1;
        int minI = 0;
        int minJ = s.Length;
        int min = s.Length;

        bool isSubstring = false;

        while (j < s.Length) 
        {
            if (isSubstring)
            {
                int length = j - i + 1;
                if (length <= min) 
                {
                    min = length;
                    minI = i;
                    minJ = j;
                }

                var c = s[i];
                if (currentFrequencyMap.ContainsKey(c))
                {
                    currentFrequencyMap[c]++;
                    if (currentFrequencyMap[c] > 0)
                    {
                        isSubstring = false;
                    }
                }

                i++;
            }
            else
            {
                j++;
                if (j < s.Length)
                {
                    var c = s[j];
                    if (currentFrequencyMap.ContainsKey(c))
                    {
                        currentFrequencyMap[c]--;
                    }

                    isSubstring = currentFrequencyMap.All(pair => pair.Value <= 0);
                }
            }
        }

        if (minI < s.Length && minJ < s.Length)
        {
            return s.Substring(minI, minJ - minI + 1);
        }
        else
        {
            return string.Empty;
        }
    }
}