public class Solution
{
    public int HeightChecker(int[] heights)
    {
        var sorted = new int[heights.Length];
        Array.Copy(heights, sorted, heights.Length);
        Array.Sort(sorted);

        int count = 0;

        for (int i = 0; i < sorted.Length; i++)
        {
            if (heights[i] != sorted[i])
            {
                count++;
            }
        }

        return count;
    }
}