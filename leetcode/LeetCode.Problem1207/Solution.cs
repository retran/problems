public class Solution 
{
    public bool UniqueOccurrences(int[] arr) 
    {
        var occurences = new Dictionary<int, int>();

        for (int i = 0; i < arr.Length; i++)
        {
            if (!occurences.TryGetValue(arr[i], out var count))
            {
                count = 0;
            }

            occurences[arr[i]] = count + 1;
        }

        var set = new HashSet<int>();

        foreach (var count in occurences.Values)
        {
            if (!set.Add(count))
            {
                return false;
            }
        }

        return true;
    }
}