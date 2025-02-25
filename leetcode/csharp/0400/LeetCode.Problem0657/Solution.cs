public class Solution
{
    public bool JudgeCircle(string moves)
    {
        int l = 0, r = 0, u = 0, d = 0;

        for (int i = 0; i < moves.Length; i++)
        {
            switch (moves[i])
            {
                case 'U':
                    u++;
                    break;
                case 'D':
                    d++;
                    break;
                case 'L':
                    l++;
                    break;
                default:
                    r++;
                    break;

            }
        }

        return l == r && u == d;
    }
}