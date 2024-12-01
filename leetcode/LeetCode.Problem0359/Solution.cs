public class Logger
{
    private Dictionary<string, int> _cache;

    public Logger()
    {
        _cache = new Dictionary<string, int>();
    }

    public bool ShouldPrintMessage(int timestamp, string message)
    {
        if (!_cache.TryGetValue(message, out var lastTimeStamp) || timestamp >= lastTimeStamp + 10)
        {
            _cache[message] = timestamp;
            return true;
        }

        return false;
    }
}

/**
 * Your Logger object will be instantiated and called as such:
 * Logger obj = new Logger();
 * bool param_1 = obj.ShouldPrintMessage(timestamp,message);
 */