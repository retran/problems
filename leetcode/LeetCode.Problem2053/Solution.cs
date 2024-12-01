public class Solution
{
    public string KthDistinct(string[] arr, int k)
    {
        var frequencies = new Dictionary<string, int>();
        var uniques = new List<string>();

        for (int i = 0; i < arr.Length; i++)
        {
            if (!frequencies.TryGetValue(arr[i], out var frequency))
            {
                frequency = 0;
                uniques.Add(arr[i]);
            }

            frequencies[arr[i]] = frequency + 1;
        }

        foreach (var value in uniques)
        {
            if (frequencies[value] == 1)
            {
                k--;
                if (k == 0)
                {
                    return value;
                }
            }
        }

        return string.Empty;
    }
}