using System.Runtime.Intrinsics.X86;

public class Solution
{
    private Dictionary<int, int> _bitCount = new Dictionary<int, int>();

    public int[] SortByBits(int[] arr)
    {
        Array.Sort(arr, (a, b) =>
        {
            int countA = CountBits(a);
            int countB = CountBits(b);
            if (countA == countB)
            {
                return a - b;
            }
            return countA - countB;
        });

        return arr;
    }

    private int CountBits(int value)
    {        
        if (_bitCount.TryGetValue(value, out int count))
        {
            return count;
        }

        count = (int)Popcnt.PopCount((uint)value);;

        _bitCount[value] = count;
        return count;
    }
}
