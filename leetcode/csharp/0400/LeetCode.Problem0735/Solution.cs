public class Solution
{
    public int[] AsteroidCollision(int[] asteroids)
    {
        var stack = new Stack<int>();

        int i = 0;
        while (i < asteroids.Length)
        {
            if (stack.Count == 0)
            {
                stack.Push(asteroids[i]);
                i++;
                continue;
            }

            if (stack.Peek() < 0)
            {
                stack.Push(asteroids[i]);
                i++;
                continue;
            }

            if (asteroids[i] > 0)
            {
                stack.Push(asteroids[i]);
                i++;
                continue;
            }

            while (stack.Count > 0 && stack.Peek() > 0)
            {
                var second = stack.Pop();
                if (Math.Abs(second) == Math.Abs(asteroids[i]))
                {
                    i++;
                    break;
                }
                
                if (Math.Abs(second) > Math.Abs(asteroids[i]))
                {
                    stack.Push(second);
                    i++;
                    break;
                }
            }
        }

        var result = stack.ToList();
        result.Reverse();
        return result.ToArray();
    }
}