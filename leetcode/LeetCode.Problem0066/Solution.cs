public class Solution
{
    public int[] PlusOne(int[] digits)
    {
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits; // No carry-over, return immediately
            }

            digits[i] = 0; // Carry-over
        }

        // All digits were 9, so create a new array with an extra 1
        int[] result = new int[digits.Length + 1];
        result[0] = 1;
        return result;
    }
}