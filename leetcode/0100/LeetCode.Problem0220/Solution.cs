
public class Solution {
    public bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff) {
        if (nums == null || nums.Length < 2 || indexDiff <= 0 || valueDiff < 0) {
            return false; // Edge case checks
        }

        SortedSet<long> sortedSet = new SortedSet<long>(); // Use long to handle potential overflow
        
        for (int i = 0; i < nums.Length; i++) {
            // Maintain the window size
            if (i > indexDiff) {
                sortedSet.Remove(nums[i - indexDiff - 1]); // Remove the element that's out of the window
            }

            // Check if there's a value in the sorted set within the required range
            long currentNum = (long)nums[i]; // Use long to prevent overflow in calculations
            if (sortedSet.Count > 0) {
                // Find the lower and upper bounds
                if (sortedSet.GetViewBetween(currentNum - valueDiff, currentNum + valueDiff).Count > 0) {
                    return true; // Found at least one number in the range
                }
            }

            // Add the current number to the sorted set
            sortedSet.Add(currentNum);
        }

        return false; // If no valid pair was found
    }
}
