
public class DisjointSet
{
    private readonly int[] parents;
    private readonly int[] ranks;

    private int count;

    public DisjointSet(int size)
    {
        parents = new int[size];
        ranks = new int[size];

        for (int i = 0; i < size; i++)
        {
            parents[i] = i;
            ranks[i] = 0;
        }

        count = size;
    }

    public bool AreAllSetsJoined()
    {
        return count == 1;
    }

    public int Find(int x)
    {
        if (parents[x] != x)
        {
            parents[x] = Find(parents[x]); // путь сжатие
        }
        return parents[x];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            if (ranks[rootX] > ranks[rootY])
            {
                parents[rootY] = rootX;
            }
            else if (ranks[rootX] < ranks[rootY])
            {
                parents[rootX] = rootY;
            }
            else
            {
                parents[rootY] = rootX;
                ranks[rootX]++;
            }

            count--;
        }
    }
}

public class Solution
{
    public int EarliestAcq(int[][] logs, int n)
    {
        var set = new DisjointSet(n);
        
        foreach (var entry in logs.OrderBy(e => e[0]))
        {
            set.Union(entry[1], entry[2]);
            if (set.AreAllSetsJoined())
            {
                return entry[0];
            }
        }

        return -1;
    }
}