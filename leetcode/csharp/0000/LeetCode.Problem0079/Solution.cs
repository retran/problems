public class Solution
{
    private bool Backtrack(char[][] board, bool[,] visited, int i, int j, string word, int index)
    {
        if (index == word.Length)
        {
            return true;
        }

        if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length || visited[i, j] || board[i][j] != word[index])
        {
            return false;
        }

        visited[i, j] = true;
        if (Backtrack(board, visited, i + 1, j, word, index + 1) ||
            Backtrack(board, visited, i - 1, j, word, index + 1) ||
            Backtrack(board, visited, i, j + 1, word, index + 1) ||
            Backtrack(board, visited, i, j - 1, word, index + 1))
        {
            return true;
        }
        visited[i, j] = false;

        return false;
    }
    
    public bool Exist(char[][] board, string word)
    {
        int m = board.Length;
        int n = board[0].Length;
        bool[,] visited = new bool[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (Backtrack(board, visited, i, j, word, 0))
                {
                    return true;
                }
            }
        }
        return false;
    }
}