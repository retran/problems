// https://leetcode.com/problems/robot-collisions

// alternate solution - Stack, similar to nested brackets problems

public class Solution
{
    public IList<int> SurvivedRobotsHealths(int[] positions, int[] healths, string directions)
    {
        var indicies = Enumerable.Range(0, positions.Length).ToList().OrderBy(i => positions[i]).ToList();
        int i = 0;
        int j = 1;

        while (i < indicies.Count && j < indicies.Count)
        {
            while (i < indicies.Count && (directions[indicies[i]] == 'L' || healths[indicies[i]] == 0))
            {
                i++;
            }

            if (i == indicies.Count)
            {
                break;
            }

            if (j <= i)
            {
                j = i + 1;
            }

            while (j < indicies.Count && healths[indicies[j]] == 0)
            {
                j++;
            }

            if (j < indicies.Count && directions[indicies[j]] == 'L')
            {
                if (healths[indicies[j]] < healths[indicies[i]])
                {
                    healths[indicies[i]]--;
                    healths[indicies[j]] = 0;
                }
                else if (healths[indicies[j]] > healths[indicies[i]])
                {
                    healths[indicies[i]] = 0;
                    healths[indicies[j]]--;
                }
                else
                {
                    healths[indicies[i]] = 0;
                    healths[indicies[j]] = 0;
                }

                while (i > 0 && healths[indicies[i]] == 0)
                {
                    i--;
                }

                while (j < indicies.Count && healths[indicies[j]] == 0)
                {
                    j++;
                }
            }
            else
            {
                i++;
            }
        }

        return healths.Where(h => h > 0).ToList();
    }
}