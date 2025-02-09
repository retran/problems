public class Solution
{
    public int CountStudents(int[] students, int[] sandwiches)
    {
        int count0 = students.Count(s => s == 0);
        int count1 = students.Count(s => s == 1);
        
        foreach (int sandwich in sandwiches)
        {
            if (sandwich == 0)
            {
                if (count0 > 0)
                    count0--;
                else
                    break;
            }
            else
            {
                if (count1 > 0)
                    count1--;
                else
                    break;
            }
        }
        
        return count0 + count1;
    }
}
