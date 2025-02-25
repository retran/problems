public class Solution
{
    public int LargestPathValue(string colors, int[][] edges)
    {
        int[] inDegree = new int[colors.Length];
        List<int>[] connections = new List<int>[colors.Length];
        int[,] colorFrequencies = new int[colors.Length, 26];
        
        for (int i = 0; i < colors.Length; i++)
        {
            connections[i] = new List<int>();
        }
        
        foreach (var edge in edges)
        {
            connections[edge[0]].Add(edge[1]);
            inDegree[edge[1]]++;
        }
        
        Queue<int> queue = new Queue<int>();
        
        for (int i = 0; i < colors.Length; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }
        
        int visited = 0;
        int maxColorCount = 0;
        
        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            visited++;
            
            colorFrequencies[node, colors[node] - 'a']++;
            maxColorCount = Math.Max(maxColorCount, colorFrequencies[node, colors[node] - 'a']);
            
            foreach (int neighbor in connections[node])
            {
                for (int c = 0; c < 26; c++)
                {
                    colorFrequencies[neighbor, c] = Math.Max(colorFrequencies[neighbor, c], colorFrequencies[node, c]);
                }
                
                inDegree[neighbor]--;

                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
        
        return visited == colors.Length ? maxColorCount : -1;
    }
}
