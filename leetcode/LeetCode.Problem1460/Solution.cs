public class Solution
{
    public bool CanBeEqual(int[] target, int[] arr)
    {
        var frequencies = new Dictionary<int, int>();
        for (int i = 0; i < target.Length; i++)
        {
            if (frequencies.TryGetValue(target[i], out var count))
            {
                count++;
            }
            else
            {
                count = 1;
            }
            frequencies[target[i]] = count;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            if (!frequencies.TryGetValue(arr[i], out var count) || count < 1)
            {
                return false;
            }
            else
            {
                frequencies[arr[i]]--;
            }
        }

        return true;
    }
}