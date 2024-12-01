public class Solution
{
    public int EraseOverlapIntervals(int[][] intervals)
    {
        intervals = intervals
            .OrderBy(i => i[1])
            .ToArray();

        int count = 0;
        int k = int.MinValue;
        
        for (int i = 0; i < intervals.Length; i++) {
            int x = intervals[i][0];
            int y = intervals[i][1];
            
            if (x >= k) {
                k = y;
            } else {
                count++;
            }
        }
        
        return count;
   }
}