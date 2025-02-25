public class Solution
{
    public int NumTeams(int[] rating)
    {
        int n = rating.Length;
        int teamsCount = 0;

        for (int i = 0; i < rating.Length; i++)
        {
            int leftSmaller = 0;
            int leftBigger = 0;
            int rightSmaller = 0;
            int rightBigger = 0;

            for (int j = 0; j < i; j++)
            {
                if (rating[j] < rating[i])
                {
                    leftSmaller++;
                }
                else if (rating[j] > rating[i])
                {
                    leftBigger++;
                }
            }

            for (int j = i + 1; j < rating.Length; j++)
            {
                if (rating[j] < rating[i])
                {
                    rightSmaller++;
                }
                else if (rating[j] > rating[i])
                {
                    rightBigger++;
                }
            }

            teamsCount += leftSmaller * rightBigger + leftBigger * rightSmaller;
        }

        return teamsCount;
    }
}