public class Solution
{
    public string[] SortPeople(string[] names, int[] heights)
    {
        var people = new List<(string Name, int Height)>();
        for (int i = 0; i < names.Length; i++)
        {
            people.Add((names[i], heights[i]));
        }

        var namesSorted = people
            .OrderByDescending(p => p.Height)
            .ThenBy(p => p.Name)
            .Select(p => p.Name)
            .ToArray();

        return namesSorted;
    }
}