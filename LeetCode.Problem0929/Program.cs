using System.Diagnostics;
using System.Text;

var solution = new Solution();

Debug.Assert(solution.NumUniqueEmails(["test.email+alex@leetcode.com","test.e.mail+bob.cathy@leetcode.com","testemail+david@lee.tcode.com"]) == 2);
Debug.Assert(solution.NumUniqueEmails(["a@leetcode.com","b@leetcode.com","c@leetcode.com"]) == 3);

public class Solution
{
    enum State
    {
        ReadingLocalName,
        SkippingLocalNameEnding,
        ReadingDomainName
    }

    public string ToCanonicalEmail(string email)
    {
        var sb = new StringBuilder();
        var state = State.ReadingLocalName;
        foreach (var symbol in email)
        {
            switch (state)
            {
                case State.ReadingLocalName:
                    switch (symbol)
                    {
                        case '+':
                            state = State.SkippingLocalNameEnding;
                            break;
                        case '.':
                            break;
                        case '@':
                            sb.Append(symbol);
                            state = State.ReadingDomainName;
                            break;
                        default:
                            sb.Append(symbol);
                            break;
                    }
                    break;
                case State.SkippingLocalNameEnding:
                    switch (symbol)
                    {
                        case '@':
                            sb.Append(symbol);
                            state = State.ReadingDomainName;
                            break;
                        default:
                            break;
                    }
                    break;
                case State.ReadingDomainName:
                    sb.Append(symbol);
                    break;
            }
        }

        return sb.ToString();
    }

    public int NumUniqueEmails(string[] emails)
    {
        if (emails.Length < 2)
        {
            return emails.Length;
        }

        var set = new HashSet<string>();

        foreach (var email in emails)
        {
            set.Add(ToCanonicalEmail(email));
        }

        return set.Count();
    }
}