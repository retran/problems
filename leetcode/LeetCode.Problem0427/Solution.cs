public class Solution
{
    public Node Construct(int[][] grid, int i, int j, int size)
    {
        if (size == 1)
        {
            return new Node(grid[i][j] == 1, true);
        }

        var topLeft = Construct(grid, i, j, size / 2);
        var topRight = Construct(grid, i, j + size / 2, size / 2);
        var bottomLeft = Construct(grid, i + size / 2, j, size / 2);
        var bottomRight = Construct(grid, i + size / 2, j + size / 2, size / 2);

        if (topLeft.isLeaf && topRight.isLeaf && bottomLeft.isLeaf && bottomRight.isLeaf &&
            topLeft.val == topRight.val && topRight.val == bottomLeft.val && bottomLeft.val == bottomRight.val)
        {
            return new Node(topLeft.val, true);
        }

        return new Node(true, false, topLeft, topRight, bottomLeft, bottomRight);
    }

    public Node Construct(int[][] grid)
    {
        return Construct(grid, 0, 0, grid.Length);
    }
}