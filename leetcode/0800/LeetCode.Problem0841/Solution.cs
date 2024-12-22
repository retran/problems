// https://leetcode.com/problems/keys-and-rooms

public class Solution
{
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        bool[] visited = new bool[rooms.Count];
        var stack = new Stack<int>();
        stack.Push(0);

        while (stack.Count > 0)
        {
            var room = stack.Pop();
            visited[room] = true;

            foreach (var key in rooms[room])
            {
                if (!visited[key])
                {
                    stack.Push(key);
                }
            }
        }

        for (int i = 0; i < visited.Length; i++)
        {
            if (!visited[i])
            {
                return false;
            }
        }

        return true;
    }
}