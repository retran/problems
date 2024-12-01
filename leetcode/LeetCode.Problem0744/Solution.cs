public class Solution
{
    public char NextGreatestLetter(char[] letters, char target)
    {
        if (letters.Length < 0)
        {
            return (char)0;
        }

        int left = 0;
        int right = letters.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (letters[mid] <= target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        if (letters[left] > target)
        {
            return letters[left];
        }
        else
        {
            return letters[0];
        }
    }
}