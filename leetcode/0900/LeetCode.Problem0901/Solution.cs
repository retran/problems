public class StockSpanner
{
    private Stack<(int, int)> stack = new Stack<(int, int)>();

    public StockSpanner()
    {

    }

    public int Next(int price)
    {
        int count = 0;
        if (stack.Count == 0)
        {
            count++;
        }
        else
        {
            if (stack.Peek().Item2 > price)
            {
                count++;
            }
            else
            {
                while (stack.Count > 0 && stack.Peek().Item2 <= price)
                {
                    var (days, prevPrice) =  stack.Pop();
                    count += days;
                }
                count++;
            }
        }
        stack.Push((count, price));
        return count;
    }
}

/**
 * Your StockSpanner object will be instantiated and called as such:
 * StockSpanner obj = new StockSpanner();
 * int param_1 = obj.Next(price);
 */