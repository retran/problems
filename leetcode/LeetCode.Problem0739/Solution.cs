public class Solution
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        var answer = new int[temperatures.Length];
        var stack = new Stack<int>();


        int i = 0;
        while (i < temperatures.Length)
        {
            if (stack.Count == 0 || temperatures[stack.Peek()] >= temperatures[i])
            {
                stack.Push(i);
                i++;
            }
            else
            {
                var j = stack.Pop();
                answer[j] = i - j;
            }
        }

        return answer;
    }
}