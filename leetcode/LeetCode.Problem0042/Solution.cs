public class Solution {
    public int Trap(int[] height) {

        int[] left = new int[height.Length];
        int[] right = new int[height.Length];

        int currentLevel = -1;
        for (int i = 0; i < height.Length; i++)
        {
            if (height[i] > currentLevel)
            {
                currentLevel = height[i];
            }

            left[i] = currentLevel;
        }

        currentLevel = -1;
        for (int i = height.Length - 1; i >= 0; i--)
        {
            if (height[i] > currentLevel)
            {
                currentLevel = height[i];
            }

            right[i] = currentLevel;
        }

        int sum = 0;

        for (int i = 0; i < height.Length; i++)
        {
            var level = int.Min(left[i], right[i]);
            if (level > height[i])
            {
                sum += level - height[i];
            }
        }

        return sum;
    }
}