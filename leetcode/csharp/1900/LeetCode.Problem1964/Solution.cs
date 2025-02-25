public class Solution
{
    public int[] LongestObstacleCourseAtEachPosition(int[] obstacles)
    {
        int n = obstacles.Length;
        int[] lengths = new int[n];
        var subsequence = new List<int>();

        for (int i = 0; i < n; i++)
        {
            int left = 0, right = subsequence.Count;

            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (subsequence[mid] <= obstacles[i])
                    left = mid + 1;
                else
                    right = mid;
            }

            if (left < subsequence.Count)
                subsequence[left] = obstacles[i];
            else
                subsequence.Add(obstacles[i]);

            lengths[i] = left + 1;
        }

        return lengths;
    }
}