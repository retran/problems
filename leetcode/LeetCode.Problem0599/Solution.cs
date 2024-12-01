public class Solution
{
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        var map1 = new Dictionary<string, int>();
        var map2 = new Dictionary<string, int>();

        for (int i = 0; i < list1.Length; i++)
        {
            map1.Add(list1[i], i);
        }

        for (int i = 0; i < list2.Length; i++)
        {
            map2.Add(list2[i], i);
        }

        var sums = new Dictionary<int, List<string>>();

        int min = int.MaxValue;
        for (int i = 0; i < list1.Length; i++)
        {
            if (map1.TryGetValue(list1[i], out var index1) && map2.TryGetValue(list1[i], out var index2))
            {
                int sum = index1 + index2;

                if (sum < min)
                {
                    min = sum;
                }

                if (!sums.TryGetValue(sum, out var words))
                {
                    words = new List<string>();
                    sums[sum] = words;
                }

                words.Add(list1[i]);
            }
        }

        return sums[min].ToArray();
    }
}