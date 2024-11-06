public class Solution
{
    public double Average(int[] salary)
    {
        int min = int.MaxValue;
        int max = int.MinValue;
        double sum = 0;

        foreach (var value in salary)
        {
            min = Math.Min(min, value);
            max = Math.Max(max, value);
            sum += value;
        }

        sum = sum - min - max;
        return sum / (salary.Length - 2);
    }
}