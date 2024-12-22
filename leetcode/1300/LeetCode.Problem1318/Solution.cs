public class Solution
{
    public int MinFlips(int a, int b, int c)
    {
        int count = 0;
        while (a > 0 || b > 0 || c > 0)
        {
            var abit = a & 1;
            var bbit = b & 1;
            var cbit = c & 1;

            if ((abit | bbit) != cbit)
            {
                if (cbit == 1)
                {
                    count++;
                }
                else
                {
                    count += abit + bbit;
                }
            }
            a = a >> 1;
            b = b >> 1;
            c = c >> 1;
        }
        return count;
    }
}