public class Solution
{
    public string PredictPartyVictory(string senate)
    {
        Queue<char> queue = new Queue<char>();

        int[] counts = new int[2];
        int[] bans = new int[2];

        foreach (var c in senate)
        {
            queue.Enqueue(c);
            if (c == 'R')
            {
                counts[0]++;
            }
            else
            {
                counts[1]++;
            }
        }

        while (counts[0] > 0 && counts[1] > 0)
        {
            var senator = queue.Dequeue();
            switch (senator)
            {
                case 'R':
                    if (bans[0] > 0)
                    {
                        bans[0]--;
                        counts[0]--;
                    }
                    else
                    {
                        queue.Enqueue(senator);
                        if (counts[1] > 0)
                        {
                            bans[1]++;
                        }
                    }
                    break;
                case 'D':
                    if (bans[1] > 0)
                    {
                        bans[1]--;
                        counts[1]--;
                    }
                    else
                    {
                        queue.Enqueue(senator);
                        if (counts[0] > 0)
                        {
                            bans[0]++;
                        }
                    }
                    break;
            }
        }

        if (counts[0] > 0)
        {
            return "Radiant";
        }
        else
        {
            return "Dire";
        }
    }
}