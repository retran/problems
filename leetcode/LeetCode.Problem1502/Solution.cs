public class Solution
{
    public bool CanMakeArithmeticProgression(int[] arr)
    {
        Array.Sort(arr);
        for (int i = 1; i < arr.Length - 1; i++)
        {
            int prevDiff = Math.Abs(arr[i] - arr[i - 1]);
            int nextDiff = Math.Abs(arr[i] - arr[i + 1]);
            
            if (prevDiff != nextDiff)
            {
                return false;
            }
        }

        return true;
    }
}