using System.Diagnostics;

var solution = new Solution();

Debug.Assert(solution.NextGreatestLetter([], 'a') == (char)0);
Debug.Assert(solution.NextGreatestLetter(['c','f','j'], 'a') == 'c');
Debug.Assert(solution.NextGreatestLetter(['c','f','j'], 'c') == 'f');
Debug.Assert(solution.NextGreatestLetter(['x','x','y','y'], 'z') == 'x');

public class Solution
{
    public char NextGreatestLetter(char[] letters, char target)
    {
        if (letters.Length == 0)
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