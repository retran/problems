public class Solution
{
    public bool ValidWordSquare(IList<string> words)
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].Length > words.Count)
            {
                return false;
            }

            for (int j = 0; j < words[i].Length; j++)
            {
                if (words[j].Length - 1 < i)
                {
                    return false;
                }
                
                if (words[i][j] != words[j][i]) 
                {
                    return false;
                }
            }
        }

        return true;
    }
}