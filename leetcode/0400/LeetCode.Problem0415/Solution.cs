using System.Text;

public class Solution
{
    public string AddStrings(string num1, string num2)
    {
        if (num2.Length > num1.Length)
        {
            (num2, num1) = (num1, num2);
        }

        int carry = 0;

        var sb = new StringBuilder();
        for (int i = 0; i < num1.Length; i++)
        {
            int digit1 = num1[num1.Length - i - 1] - '0';
            int digit2 = i < num2.Length ? num2[num2.Length - i -1] - '0' : 0;

            var sum = digit1 + digit2 + carry;
            sb.Insert(0, sum % 10);
            carry = sum / 10;
        }

        if (carry > 0)
        sb.Insert(0, carry);

        return sb.ToString();
    }
}