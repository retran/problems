public class Solution
{
    public int MinMeetingRooms(int[][] intervals)
    {
        if (intervals.Length == 0)
        {
            return 0;
        }

        int[] startTimes = intervals.Select(i => i[0]).OrderBy(time => time).ToArray();
        int[] endTimes = intervals.Select(i => i[1]).OrderBy(time => time).ToArray();

        int maxRooms = 0;
        int currentRooms = 0;
        int i = 0;
        int j = 0;

        while (i < startTimes.Length)
        {
            if (startTimes[i] < endTimes[j])
            {
                currentRooms++;
                maxRooms = Math.Max(maxRooms, currentRooms);
                i++;
            }
            else
            {
                currentRooms--;
                j++;
            }
        }

        return maxRooms;
    }
}
