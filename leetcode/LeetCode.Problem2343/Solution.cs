using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] SmallestTrimmedNumbers(string[] nums, int[][] queries) {
        int numCount = nums.Length;
        int queryCount = queries.Length;
        int[] results = new int[queryCount];

        // Prepare for each query
        for (int q = 0; q < queryCount; q++) {
            int trim = queries[q][1];
            int k = queries[q][0];

            // Create a list to hold trimmed numbers with their original index
            List<(string trimmedNum, int originalIndex)> trimmedNums = new List<(string, int)>();

            // Trim the numbers based on the query
            for (int i = 0; i < numCount; i++) {
                // Trim the last 'trim' digits
                string trimmed = nums[i].Substring(nums[i].Length - trim);
                trimmedNums.Add((trimmed, i));
            }

            // Sort trimmed numbers first by the trimmed value, then by original index
            trimmedNums.Sort((a, b) => {
                int comparison = string.Compare(a.trimmedNum, b.trimmedNum);
                return comparison == 0 ? a.originalIndex.CompareTo(b.originalIndex) : comparison;
            });

            // Get the k-th smallest number's original index
            results[q] = trimmedNums[k - 1].originalIndex; // Convert to 0-based index
        }

        return results;
    }
}
