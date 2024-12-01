public class Solution
{
    private class Token
    {
        public TokenType Type { get; set; }
        public int Value { get; set; }

        public Token(TokenType type, int value = -1)
        {
            Type = type;
            Value = value;
        }
    }

    private enum TokenType
    {
        Number,
        Plus,
        Minus,
        Multiply,
        Divide,
        OpenParenthesis,
        CloseParenthesis
    }

    private IEnumerable<Token> Tokenize(string s)
    {
        int index = 0;
        while (index < s.Length)
        {
            char c = s[index];
            switch (c)
            {
                case '+':
                    yield return new Token(TokenType.Plus);
                    break;
                case '*':
                    yield return new Token(TokenType.Multiply);
                    break;
                case '/':
                    yield return new Token(TokenType.Divide);
                    break;
                case '(':
                    yield return new Token(TokenType.OpenParenthesis);
                    break;
                case ')':
                    yield return new Token(TokenType.CloseParenthesis);
                    break;
                case '-':
                    yield return new Token(TokenType.Minus);
                    break;
                default:
                    StringBuilder sb = new StringBuilder();
                    while (index < s.Length && char.IsDigit(s[index]))
                    {
                        sb.Append(s[index]);
                        index++;
                    }
                    if (sb.Length > 0)
                    {
                        yield return new Token(TokenType.Number, int.Parse(sb.ToString()));
                        index--;
                    }
                    break;
            }
            index++;
        }
    }

    private int Evaluate(IEnumerable<Token> expression)
    {
        var tokens = expression.ToArray();
        int current = 0;
        return Expression(tokens, ref current);
    }

    private int Expression(Token[] tokens, ref int current)
    {
        var left = Term(tokens, ref current);
        while (current < tokens.Length)
        {
            var currentToken = tokens[current];
            if (currentToken.Type == TokenType.Plus)
            {
                current++;
                left += Term(tokens, ref current);
            }
            else if (currentToken.Type == TokenType.Minus)
            {
                current++;
                left -= Term(tokens, ref current);
            }
            else
            {
                break;
            }
        }
        return left;
    }

    private int Term(Token[] tokens, ref int current)
    {
        var left = Factor(tokens, ref current);
        while (current < tokens.Length)
        {
            var currentToken = tokens[current];
            if (currentToken.Type == TokenType.Multiply)
            {
                current++;
                left *= Factor(tokens, ref current);
            }
            else if (currentToken.Type == TokenType.Divide)
            {
                current++;
                left /= Factor(tokens, ref current);
            }
            else
            {
                break;
            }
        }
        return left;
    }

    private int Factor(Token[] tokens, ref int current)
    {
        var currentToken = tokens[current];
        if (currentToken.Type == TokenType.Number)
        {
            current++;
            return currentToken.Value;
        }
        else if (currentToken.Type == TokenType.Minus)
        {
            current++;
            return -Factor(tokens, ref current);
        }
        else if (currentToken.Type == TokenType.OpenParenthesis)
        {
            current++;
            var result = Expression(tokens, ref current);
            current++;
            return result;
        }
        return 0;
    }

    public int Calculate(string s)
    {
        return Evaluate(Tokenize(s));
    }
}