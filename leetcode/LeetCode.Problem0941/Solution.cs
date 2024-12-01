public class Solution
{
    public bool ValidMountainArray(int[] arr)
    {
        if (arr.Length < 3)
        {
            return false;
        }

        int i = 0;
        for (i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i] == arr[i + 1])
            {
                return false;
            }

            if (arr[i] > arr[i + 1])
            {
                break;
            }
        }

        if (i == 0 || i == arr.Length - 1)
        {
            return false;
        }

        for (; i < arr.Length - 1; i++)
        {
            if (arr[i] <= arr[i + 1])
            {
                return false;
            }
        }

        return true;
    }
}