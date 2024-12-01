public class Solution
{
    public int MaxDistance(IList<IList<int>> arrays)
    {
        int[] maxes = new int[arrays.Count];
        int[] mines = new int[arrays.Count];

        for (int i = 0; i < arrays.Count; i++)
        {
            mines[i] = arrays[i].First();
            maxes[i] = arrays[i].Last();
        }

        int firstMin = mines[0] <= mines[1] ? 0 : 1;
        int secondMin = mines[0] <= mines[1] ? 1 : 0;
        int firstMax = maxes[0] >= maxes[1] ? 0 : 1;
        int secondMax = maxes[0] >= maxes[1] ? 1 : 0;

        for (int i = 2; i < maxes.Length; i++)
        {
            if (mines[i] <= mines[firstMin])
            {
                secondMin = firstMin;
                firstMin = i;
            }
            else if (mines[i] <= mines[secondMin])
            {
                secondMin = i;
            }

            if (maxes[i] >= maxes[firstMax])
            {
                secondMax = firstMax;
                firstMax = i;
            }
            else if (maxes[i] >= maxes[secondMax])
            {
                secondMax = i;
            }
        }

        if (firstMax != firstMin)
        {
            return maxes[firstMax] - mines[firstMin];
        }
        else
        {
            return Math.Max(maxes[firstMax] - mines[secondMin], maxes[secondMax] - mines[firstMin]);
        }
    }
}