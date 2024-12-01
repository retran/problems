public class Solution
{
    public int FindCircleNum(int[][] isConnected)
    {
        int provinces = 0;
        int[] provincesMap = new int[isConnected.Length];

        while (true)
        {
            int start = -1;
            for (int i = 0; i < provincesMap.Length; i++)
            {
                if (provincesMap[i] == 0)
                {
                    start = i;
                    break;
                }
            }

            if (start == -1)
            {
                break;
            }

            provinces++;
            var queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                provincesMap[current] = provinces;

                for (int i = 0; i < isConnected.Length; i++)
                {
                    if (provincesMap[i] == 0 && isConnected[current][i] == 1)
                    {
                        queue.Enqueue(i);
                    }
                }
            }
        }

        return provinces;
    }
}