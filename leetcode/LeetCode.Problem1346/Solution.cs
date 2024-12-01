public class Solution
{
    public bool CheckIfExist(int[] arr)
    {
        var set = new HashSet<int>(arr);

        int countOfZeroes = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 0)
            {
                countOfZeroes++;
                if (countOfZeroes > 1)
                {
                    return true;
                }
            }
            else if (set.Contains(arr[i] * 2))
            {
                return true;
            }
        }

        return false;
    }
}