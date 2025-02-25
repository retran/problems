public class Solution
{
    public bool CheckStraightLine(int[][] coordinates)
    {
        int x1 = coordinates[0][0];
        int x2 = coordinates[1][0];
        int y1 = coordinates[0][1];
        int y2 = coordinates[1][1];

        if (x1 == x2)
        {
            for (int i = 2; i < coordinates.Length; i++)
            {
                if (coordinates[i][0] != x1)
                {
                    return false;
                }
            }
        }
        else if (y1 == y2)
        {
            for (int i = 2; i < coordinates.Length; i++)
            {
                if (coordinates[i][1] != y1)
                {
                    return false;
                }
            }
        }
        else
        {
            double k = (y2 - y1) / (double)(x2 - x1);
            double a = (double)(y1 + y2 - k * (x1 + x2)) / 2;

            for (int i = 2; i < coordinates.Length; i++)
            {
                int x = coordinates[i][0];
                double y = k * x + a;
                if (Math.Abs(y - coordinates[i][1]) > double.Epsilon)
                {
                    return false;
                }
            }
        }

        return true;
    }
}