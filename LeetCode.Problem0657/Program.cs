public class Solution
{
    public bool JudgeCircle(string moves)
    {
        int l = 0, r = 0, u = 0, d = 0;

        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i] == 'R')
            {
                r++;
            }
            else if (moves[i] == 'L')
            {
                l++;
            }
            else if (moves[i] == 'U')
            {
                u++;
            }
            else
            {
                d++;
            }
        }

        return l == r && u == d;
    }
}