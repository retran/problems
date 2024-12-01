public class Solution
{
    public int[] TimeTaken(int[] arrival, int[] state)
    {
        var enteringQueue = new Queue<(int, int)>();
        var exitingQueue = new Queue<(int, int)>();

        int[] customerTimes = new int[arrival.Length];

        for (int i = 0; i < arrival.Length; i++)
        {
            if (state[i] == 0)
            {
                enteringQueue.Enqueue((arrival[i], i));
            }
            else
            {
                exitingQueue.Enqueue((arrival[i], i));
            }
        }

        int lastCustomerState = -1;
        int lastCustomerTime = -1;
        while (enteringQueue.Count > 0 || exitingQueue.Count > 0)
        {
            (int, int) currentCustomer;
            int nextEventTime = -1;

            if (enteringQueue.Count == 0)
            {
                currentCustomer = exitingQueue.Dequeue();
                nextEventTime = currentCustomer.Item1;
                lastCustomerState = 1;
            }
            else if (exitingQueue.Count == 0)
            {
                currentCustomer = enteringQueue.Dequeue();
                nextEventTime = currentCustomer.Item1;
                lastCustomerState = 0;
            }
            else
            {
                var enteringTime = enteringQueue.Peek().Item1;
                var exitingTime = exitingQueue.Peek().Item1;

                if (enteringTime <= lastCustomerTime)
                {
                    enteringTime = lastCustomerTime + 1;
                }

                if (exitingTime <= lastCustomerTime)
                {
                    exitingTime = lastCustomerTime + 1;
                }

                if (enteringTime < exitingTime)
                {
                    currentCustomer = enteringQueue.Dequeue();
                    nextEventTime = enteringTime;
                    lastCustomerState = 0;
                }
                else if (enteringTime > exitingTime)
                {
                    currentCustomer = exitingQueue.Dequeue();
                    nextEventTime = exitingTime;
                    lastCustomerState = 1;
                }
                else
                {
                    nextEventTime = exitingTime;
                    if (nextEventTime - lastCustomerTime == 1)
                    {
                        if (lastCustomerState == 0)
                        {
                            currentCustomer = enteringQueue.Dequeue();
                            lastCustomerState = 0;
                        }
                        else
                        {
                            currentCustomer = exitingQueue.Dequeue();
                            lastCustomerState = 1;
                        }
                    }
                    else
                    {
                        currentCustomer = exitingQueue.Dequeue();
                        lastCustomerState = 1;
                    }
                }
            }

            if (lastCustomerTime < nextEventTime)
            {
                lastCustomerTime = nextEventTime;
            }
            else
            {
                lastCustomerTime++;
            }

            customerTimes[currentCustomer.Item2] = lastCustomerTime;
        }

        return customerTimes;
    }
}