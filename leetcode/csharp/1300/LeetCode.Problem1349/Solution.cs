using System.Runtime.Intrinsics.X86;

public class Solution
{
    private int[] GetBrokenSeatsMasks(char[][] seats)
    {
        int rows = seats.Length;
        int cols = seats[0].Length;
        int[] brokenSeats = new int[rows];

        for (int i = 0; i < rows; i++)
        {
            int mask = 0;
            for (int j = 0; j < cols; j++)
            {
                if (seats[i][j] == '#')
                    mask |= (1 << j);
            }
            brokenSeats[i] = mask;
        }
        return brokenSeats;
    }

    public int MaxStudents(char[][] seats)
    {
        int rows = seats.Length;
        int cols = seats[0].Length;
        int[] brokenSeats = GetBrokenSeatsMasks(seats);

        int totalMasks = 1 << cols;
        int[] prevRow = new int[totalMasks];
        int[] curRow = new int[totalMasks];

        List<int>[] validPrevMasks = new List<int>[totalMasks];

        for (int curMask = 0; curMask < totalMasks; curMask++)
        {
            validPrevMasks[curMask] = new List<int>();

            if ((curMask & (curMask >> 1)) != 0)
                continue;

            for (int prevMask = 0; prevMask < totalMasks; prevMask++)
            {
                if ((prevMask & (curMask >> 1)) == 0 && (prevMask & (curMask << 1)) == 0)
                    validPrevMasks[curMask].Add(prevMask);
            }
        }

        int maxStudents = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int curMask = 0; curMask < totalMasks; curMask++)
            {
                curRow[curMask] = 0;

                if ((curMask & (curMask >> 1)) != 0 || (curMask & brokenSeats[row]) != 0)
                    continue;

                int studentCount = CountSetBits(curMask);

                foreach (int prevMask in validPrevMasks[curMask])
                {
                    curRow[curMask] = Math.Max(curRow[curMask], prevRow[prevMask] + studentCount);
                }

                maxStudents = Math.Max(maxStudents, curRow[curMask]);
            }

            var temp = prevRow;
            prevRow = curRow;
            curRow = temp;
        }

        return maxStudents;
    }

    private int CountSetBits(int num)
    {
        return Popcnt.IsSupported ? (int)Popcnt.PopCount((uint)num) : SoftwareBitCount(num);
    }

    private int SoftwareBitCount(int num)
    {
        int count = 0;
        while (num > 0)
        {
            num &= num - 1;
            count++;
        }
        return count;
    }
}
