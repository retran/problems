public class Solution {
    public int MaximumWealth(int[][] accounts) 
    {
        int max = 0;

        for (int i = 0; i < accounts.Length; i++)
        {
            var sum = accounts[i].Sum();
            max = Math.Max(max, sum);
        }

        return max;
    }
}