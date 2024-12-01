public class Solution
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int[] currents = new int[gas.Length];

        currents[0] = gas[gas.Length - 1] - cost[gas.Length - 1];
        for (int i = 1; i < currents.Length; i++)
        {
            currents[i] = currents[i - 1] + gas[i - 1] - cost[i - 1];
        }

        int startIndex = 0;
        for (int i = 0; i < currents.Length; i++)
        {
            if (currents[i] < currents[startIndex])
            {
                startIndex = i;
            }
        }

        int currentGas = 0;
        int currentIndex = startIndex;
        bool flag = true;
        while (flag) {
            currentGas += gas[currentIndex];
            currentGas -= cost[currentIndex];

            if (currentGas < 0)
            {
                return -1;
            }

            currentIndex++;

            if (currentIndex == gas.Length)
            {
                currentIndex = 0;
            }

            if (currentIndex == startIndex)
            {
                flag = false;
            }
        }

        return startIndex;
    }
}