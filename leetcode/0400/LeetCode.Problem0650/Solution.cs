public class Solution
{
    public int MinSteps(int n)
    {
        int steps = 0;
        int factor = 2;

        while (n > 1)
        {
            while (n % factor == 0)
            {
                steps += factor;
                n /= factor;
            }
            factor++;
        }

        return steps;
    }
}
