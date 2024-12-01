public class Solution
{
    public int MinCostII(int[][] costs)
    {
        if (costs.Length == 0 || costs[0].Length == 0)
        {
            return 0;
        }

        int n = costs.Length;
        int k = costs[0].Length;

        int prevMin1 = 0, prevMin2 = 0, prevMin1Index = -1;

        for (int i = 0; i < n; i++)
        {
            int currMin1 = int.MaxValue, currMin2 = int.MaxValue, currMin1Index = -1;

            for (int j = 0; j < k; j++)
            {
                int cost = costs[i][j];
                
                if (j == prevMin1Index)
                {
                    cost += prevMin2;
                }
                else
                {
                    cost += prevMin1;
                }

                if (cost < currMin1)
                {
                    currMin2 = currMin1;
                    currMin1 = cost;
                    currMin1Index = j;
                }
                else if (cost < currMin2)
                {
                    currMin2 = cost;
                }
            }

            prevMin1 = currMin1;
            prevMin2 = currMin2;
            prevMin1Index = currMin1Index;
        }

        return prevMin1;
    }
}
