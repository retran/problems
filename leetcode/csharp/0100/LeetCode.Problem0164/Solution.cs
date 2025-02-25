using System;

public class Solution {
    public int MaximumGap(int[] nums) {
        // Edge case: if there are less than 2 numbers
        if (nums.Length < 2) {
            return 0;
        }

        int minValue = int.MaxValue;
        int maxValue = int.MinValue;

        // Find the minimum and maximum values in the array
        foreach (int num in nums) {
            minValue = Math.Min(minValue, num);
            maxValue = Math.Max(maxValue, num);
        }

        // Calculate the gap (bucket size) and number of buckets
        int n = nums.Length;
        int bucketSize = Math.Max(1, (maxValue - minValue) / (n - 1)); // Bucket size
        int bucketCount = (maxValue - minValue) / bucketSize + 1; // Number of buckets

        // Create buckets
        int[] bucketMin = new int[bucketCount];
        int[] bucketMax = new int[bucketCount];
        Array.Fill(bucketMin, int.MaxValue);
        Array.Fill(bucketMax, int.MinValue);

        // Distribute the numbers into buckets
        foreach (int num in nums) {
            int bucketIndex = (num - minValue) / bucketSize;
            bucketMin[bucketIndex] = Math.Min(bucketMin[bucketIndex], num);
            bucketMax[bucketIndex] = Math.Max(bucketMax[bucketIndex], num);
        }

        // Calculate the maximum gap
        int maxGap = 0;
        int previousMax = bucketMax[0];

        for (int i = 1; i < bucketCount; i++) {
            if (bucketMin[i] == int.MaxValue) {
                continue; // This bucket is empty
            }
            maxGap = Math.Max(maxGap, bucketMin[i] - previousMax);
            previousMax = bucketMax[i];
        }

        return maxGap;
    }
}
