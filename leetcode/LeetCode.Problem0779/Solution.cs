public class Solution 
{
    public int KthGrammar(int n, int k) 
    {
        if (n == 1) 
        {
            return 0;
        }

        int lengthOfPrevRow = 1 << (n - 2);

        if (k <= lengthOfPrevRow) 
        {
            return KthGrammar(n - 1, k);
        } 
        else 
        {
            return 1 - KthGrammar(n - 1, k - lengthOfPrevRow);
        }
    }
}
