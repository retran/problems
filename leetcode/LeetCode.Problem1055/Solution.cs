public class Solution {
    public int ShortestWay(string source, string target) {
        
        if (source == target)
        {
            return 1;
        }
        
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
        {
            return 0;
        }
                
        int count = 0;
        int i = 0;
        int j = 0;
        
        bool foundSubsequence = false;
        while (j < target.Length)
        {
            if (target[j] == source[i])
            {
                i++;
                j++;
                foundSubsequence = true;
            }
            else
            {
                i++;
            }
            
            if (i == source.Length)
            {
                if (!foundSubsequence)
                {
                    return -1;
                }
                else
                {
                    count++;
                    i = 0;
                    foundSubsequence = false;
                }
            }
        }
        
        if (i != 0)
        {
            if (!foundSubsequence)
            {
                return -1;
            }
            
            count++;
        }
        
        return count;
    }
}