public class Solution
{
    public int MinNumberOperations(int[] target)
    {
        int totalOperations = target[0];
        int operationsWeCanReuse = target[0];
        for (int i = 1; i < target.Length; i++)
        {
            if (target[i] <= operationsWeCanReuse)
            {
                operationsWeCanReuse = target[i];
            }
            else
            {
                totalOperations += target[i] - operationsWeCanReuse;
                operationsWeCanReuse = target[i];
            }
        }
        return totalOperations;
    }

    static void Main()
    {
        Solution solution = new Solution();
        Console.WriteLine(solution.MinNumberOperations(new int[] { 1, 2, 3, 2, 1 }));
    }
}