public class Solution
{
    public double MyPow(double x, int n)
    {
        double result = 1;
        if (n < 0) 
        {
            x = 1 / x;
            if (n == int.MinValue) 
            {
                n = int.MaxValue;
                result = 1 / x;
            }
            else
            {
                n = -n;
            }
        }
        while (n != 0) 
        {
            if (n % 2 == 1) 
            {
                result = result * x;
            }

            x = x * x;
            n = n / 2;
        }
        return result;
    }
}