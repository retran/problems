public class Solution
{
    public IList<int> FindDisappearedNumbers(int[] nums)
    {
        bool[] present = new bool[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            present[nums[i] - 1] = true;
        }

        var disappeared = new List<int>();
        for (int i = 0; i < present.Length; i++)
        {
            if (!present[i])
            {
                disappeared.Add(i + 1);
            }
        }

        return disappeared;
    }
}