
using System.Text;

public class Solution
{
    enum TokenKind
    {
        Number,
        String,
        Open,
        Close
    }

    class Token
    {
        public string Value { get; private set; }
        public TokenKind Kind { get; private set; }

        public Token(TokenKind kind, string value)
        {
            Kind = kind;
            Value = value;
        }
    }

    private IEnumerable<Token> Tokenize(string encodedString)
    {
        int index = 0;
        while (index < encodedString.Length)
        {
            if (char.IsDigit(encodedString[index]))
            {
                var number = ReadNumber(encodedString, ref index);
                yield return new Token(TokenKind.Number, number);
                continue;
            }

            if (char.IsLetter(encodedString[index]))
            {
                var chunk = ReadString(encodedString, ref index);
                yield return new Token(TokenKind.String, chunk);
                continue;
            }

            if (encodedString[index] == '[')
            {
                yield return new Token(TokenKind.Open, "[");
                index++;
                continue;
            }

            if (encodedString[index] == ']')
            {
                yield return new Token(TokenKind.Close, "]");
                index++;
                continue;
            }
        }
    }

    private string ReadString(string encodedString, ref int index)
    {
        var sb = new StringBuilder();
        while (index < encodedString.Length && char.IsLetter(encodedString[index]))
        {
            sb.Append(encodedString[index]);
            index++;
        }
        return sb.ToString();
    }

    private string ReadNumber(string encodedString, ref int index)
    {
        var sb = new StringBuilder();
        while (index < encodedString.Length && char.IsDigit(encodedString[index]))
        {
            sb.Append(encodedString[index]);
            index++;
        }
        return sb.ToString();
    }

    public string DecodeString(string s)
    {
        return Evaluate(Tokenize(s));
    }

    private string Evaluate(IEnumerable<Token> tokens)
    {
        return Expression(tokens.GetEnumerator());;
    }

    private string Expression(IEnumerator<Token> enumerator)
    {
        var stringBuilder = new StringBuilder();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Kind == TokenKind.Number)
            {
                int count = int.Parse(enumerator.Current.Value);
                enumerator.MoveNext(); // [
                var value = Expression(enumerator);
                while (count > 0)
                {
                    stringBuilder.Append(value);
                    count--;
                }
            }
            else if (enumerator.Current.Kind == TokenKind.String)
            {
                stringBuilder.Append(enumerator.Current.Value);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}