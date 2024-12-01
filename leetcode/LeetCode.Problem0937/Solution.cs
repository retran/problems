public class Solution
{
    private enum EntryKind
    {
        Letter = 0,
        Digit = 1
    }

    private class Entry
    {
        public string Id { get; private set; }
        public EntryKind Kind { get; private set; }
        public string Content { get; private set; }

        public Entry(string line)
        {
            var index = line.IndexOf(' ');
            Id = line.Substring(0, index);
            Content = line.Substring(index + 1);

            var index2 = Content.IndexOf(' ');
            var firstElement = index2 != -1 ? Content.Substring(0, index2) : Content;

            if (firstElement.All(char.IsDigit))
            {
                Kind = EntryKind.Digit;
            }
            else
            {
                Kind = EntryKind.Letter;
            }
        }

        public override string ToString()
        {
            return $"{Id} {Content}";
        }
    }


    public string[] ReorderLogFiles(string[] logs)
    {
        var entries = logs
            .Select(line => new Entry(line))
            ;

        var letterEntries = entries.Where(e => e.Kind == EntryKind.Letter)
            .OrderBy(e => e.Content)
            .ThenBy(e => e.Id);

        var digitEntries = entries.Where(e => e.Kind == EntryKind.Digit);

        return letterEntries.Concat(digitEntries).Select(e => e.ToString()).ToArray();
    }
}