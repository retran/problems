public class Solution
{
    public bool BuddyStrings(string s, string goal)
    {
        if (s.Length != goal.Length)
        {
            return false;
        }

        int[] indices = new int[2];
        int[] counts = new int[26];
        int count = 0;
        bool canSwapSameCharacters = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != goal[i])
            {
                if (count == 2)
                {
                    return false;
                }

                indices[count] = i;
                count++;
            }

            int index = s[i] - 'a';
            counts[index]++;
            if (counts[index] > 1)
            {
                canSwapSameCharacters = true;
            }
        }

        if (count == 0 && canSwapSameCharacters)
        {
            return true;
        }

        if (count == 2 && s[indices[0]] == goal[indices[1]] && s[indices[1]] == goal[indices[0]])
        {
            return true;
        }

        return false;
    }
}