using System;
using System.Collections.Generic;

public class Solution {
    public IList<IList<int>> MinimumAbsDifference(int[] arr) {
        // Sort the array
        Array.Sort(arr);
        
        IList<IList<int>> result = new List<IList<int>>();
        int minDiff = int.MaxValue;

        // Find the minimum absolute difference
        for (int i = 0; i < arr.Length - 1; i++) {
            int diff = arr[i + 1] - arr[i];
            if (diff < minDiff) {
                minDiff = diff;
            }
        }

        // Collect all pairs with the minimum absolute difference
        for (int i = 0; i < arr.Length - 1; i++) {
            int diff = arr[i + 1] - arr[i];
            if (diff == minDiff) {
                result.Add(new List<int> { arr[i], arr[i + 1] });
            }
        }

        return result;
    }
}
