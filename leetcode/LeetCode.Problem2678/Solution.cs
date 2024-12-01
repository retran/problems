public class Solution
{
    public int CountSeniors(string[] details)
    {
        return details
            .Select(detail => detail.Substring(11, 2))
            .Select(age => int.Parse(age))
            .Where(age => age > 60)
            .Count();
    }
}