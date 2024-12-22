public class Solution
{
    public int NearestExit(char[][] maze, int[] entrance)
    {
        int rows = maze.Length;
        int columns = maze[0].Length;

        (int row, int column) start = (entrance[0], entrance[1]);

        var visited = new HashSet<(int row, int column)>();

        var queue = new Queue<(int row, int column)>();
        queue.Enqueue(start);

        int count = 0;
        while (queue.Count > 0)
        {
            count++;
            int size = queue.Count();
            while (size > 0)
            {
                var current = queue.Dequeue();
                size--;

                if (visited.Contains(current))
                {
                    continue;
                }

                if (current != start)
                {
                    if (current.row == 0 || current.column == 0 || current.row == rows - 1 || current.column == columns - 1)
                    {
                        return count - 1;
                    }
                }

                visited.Add(current);
                
                if (current.row > 0 && maze[current.row - 1][current.column] == '.')
                {
                    queue.Enqueue((current.row - 1, current.column));
                }

                if (current.row < rows - 1 && maze[current.row + 1][current.column] == '.')
                {
                    queue.Enqueue((current.row + 1, current.column));
                }
                if (current.column > 0 && maze[current.row][current.column - 1] == '.')
                {
                    queue.Enqueue((current.row, current.column - 1));
                }
                if (current.column < columns - 1 && maze[current.row][current.column + 1] == '.')
                {
                    queue.Enqueue((current.row, current.column + 1));
                }
            }
        }

        return -1;
    }
}