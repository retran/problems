public class Solution 
{
    public string ConvertToBase7(int num) 
    {
        if (num == 0)
        {
            return "0";
        }
        
        var sb = new StringBuilder();
        bool negative = num < 0;

        num = Math.Abs(num);

        while (num > 0)
        {
            int mod = num % 7;
            sb.Insert(0, mod);
            num = num / 7;
        }

        if (negative)
        {
            sb.Insert(0, '-');
        }

        return sb.ToString();
    }
}