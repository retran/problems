public class Solution
{
    public int[] MinOperations(string boxes)
    {
        int n = boxes.Length;
        int[] left = new int[n];
        int[] leftCount = new int[n];

        left[0] = 0;
        leftCount[0] = boxes[0] == '1' ? 1 : 0;

        for (int i = 1; i < n; i++)
        {
            left[i] = left[i - 1] + leftCount[i - 1];
            leftCount[i] = leftCount[i - 1] + (boxes[i] == '1' ? 1 : 0);
        }

        Console.WriteLine(string.Join(",", left));
        Console.WriteLine(string.Join(",", leftCount));

        int[] right = new int[n];
        int[] rightCount = new int[n];

        right[n - 1] = 0;
        rightCount[n - 1] = boxes[n - 1] == '1' ? 1 : 0;

        for (int i = n - 2; i >= 0; i--)
        {
            right[i] = right[i + 1] + rightCount[i + 1];
            rightCount[i] = rightCount[i + 1] + (boxes[i] == '1' ? 1 : 0);
        }

        var answer = new int[n];
        for (int i = 0; i < n; i++)
        {
            answer[i] = left[i] + right[i];
        }

        return answer;
    }
}