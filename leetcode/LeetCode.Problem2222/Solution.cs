public class Solution
{
    public long NumberOfWays(string s)
    {
        long numberOfWays = 0;

        long totalZeros = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '0')
            {
                totalZeros++;
            }
        }
        
        long totalOnes = s.Length - totalZeros;
        
        long currentZeros = s[0] == '0' ? 1 : 0;
        long currentOnes = s[0] == '1' ? 1 : 0;
        
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] == '0')
            {
                numberOfWays += currentOnes * (totalOnes - currentOnes);
                currentZeros++;
            }
            else
            {
                numberOfWays += currentZeros * (totalZeros - currentZeros);
                currentOnes++;
            }
        }
        
        return numberOfWays;
    }
}
