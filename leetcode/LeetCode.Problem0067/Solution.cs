public class Solution
{
    public string AddBinary(string a, string b)
    {
        if (b.Length > a.Length) 
        {
            var temp = a;
            a = b;
            b = temp;
        }

        var stack = new Stack();

        int carry = 0;
        for (int i = a.Length - 1, j = b.Length - 1; i >= 0; i--, j--)
        {
            int sum = carry;
            sum += a[i] - '0';
            if (j >= 0)
            {
                sum += b[j] - '0';
            }

            stack.Push(sum % 2);
            carry = sum / 2;
        }

        if (carry > 0)
        {
            stack.Push(carry);
        }

        var sb = new StringBuilder();
        while (stack.Count > 0)
        {
            sb.Append(stack.Pop());
        }
        return sb.ToString();
    }
}