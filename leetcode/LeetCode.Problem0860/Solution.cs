public class Solution
{
    public bool LemonadeChange(int[] bills)
    {
        int[] count = new int[3];

        for (int i = 0; i < bills.Length; i++)
        {
            int bill = bills[i];
            if (bill == 5)
            {
                count[0]++;
            }
            else if (bill == 10)
            {
                count[1]++;
            }
            else
            {
                count[2]++;
            }

            int change = bill - 5;
            while (change > 0)
            {
                if (change < 10 || count[1] == 0)
                {
                    break;
                }
                change -= 10;
                count[1]--;
            }

            while (change > 0)
            {
                if (count[0] == 0)
                {
                    return false;
                }
                change -= 5;
                count[0]--;
            }
        }

        return true;
    }
}