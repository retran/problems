public class Solution
{
    public int[] DiStringMatch(string s)
    {
        int n = s.Length;
        int[] nums = new int[n + 1];

        void Swap(int i, int j)
        {
            int tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }

        for (int i = 0; i < n + 1; i++)
        {
            nums[i] = i;
        }

        bool changed = true;

        while (changed)
        {
            changed = false;
            for (int i = 0; i < n; i++)
            {
                if (s[i] == 'I')
                {
                    if (nums[i] > nums[i + 1])
                    {
                        Swap(i, i + 1);
                        changed = true;
                    }
                }
                else
                {
                    if (nums[i] < nums[i + 1])
                    {
                        Swap(i, i + 1);
                        changed = true;
                    }
                }
            }
        }

        return nums;
    }
}