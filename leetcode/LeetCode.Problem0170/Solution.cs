public class TwoSum
{
    private Dictionary<int, int> numCounts;

    public TwoSum()
    {
        numCounts = new Dictionary<int, int>();
    }

    public void Add(int number)
    {
        if (numCounts.ContainsKey(number))
        {
            numCounts[number]++;
        }
        else
        {
            numCounts[number] = 1;
        }
    }

    public bool Find(int value)
    {
        foreach (var key in numCounts.Keys)
        {
            int complement = value - key;
            if (complement == key)
            {
                if (numCounts[key] > 1)
                {
                    return true;
                }
            }
            else if (numCounts.ContainsKey(complement))
            {
                return true;
            }
        }
        return false;
    }
}

/**
 * Your TwoSum object will be instantiated and called as such:
 * TwoSum obj = new TwoSum();
 * obj.Add(number);
 * bool param_2 = obj.Find(value);
 */
