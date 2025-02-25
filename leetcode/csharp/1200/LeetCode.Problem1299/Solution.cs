public class Solution
{
    public int[] ReplaceElements(int[] arr)
    {
        if (arr.Length < 2)
        {
            return [-1];
        }

        int max = arr[arr.Length - 1];
        arr[arr.Length - 1] = -1;
        for (int i = arr.Length - 2; i >= 0; i--)
        {
            if (arr[i] < max)
            {
                arr[i] = max;
            }
            else
            {
                int tmp = arr[i];
                arr[i] = max;
                max = tmp;
            }
        }

        return arr;
    }
}