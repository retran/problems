public class Solution
{
    public IList<int> AddToArrayForm(int[] num, int k)
    {
        var answer = new List<int>();

        int i = num.Length - 1;

        int carry = 0;
        while (k > 0 || i >= 0)
        {
            int mod = k % 10;
            k = k / 10;

            int sum = mod + carry;

            if (i >= 0)
            {
                sum += num[i];
                i--;
            }

            carry = sum / 10;
            sum = sum % 10;

            answer.Add(sum);
        }

        if (carry != 0)
        {
            answer.Add(carry);
        }


        answer.Reverse();
        return answer;
    }
}