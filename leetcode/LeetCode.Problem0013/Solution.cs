public class Solution
{
    public int RomanToInt(string s)
    {
        var numbers = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        var result = 0;
        int i = 0;

        while (i < s.Length)
        {
            if (i == s.Length - 1)
            {
                result += numbers[s[i]];
                break;
            }
            
            switch (s[i])
            {
                case 'I':
                    if (s[i + 1] == 'V')
                    {
                        result += 4;
                        i++;
                    }
                    else if (s[i + 1] == 'X')
                    {
                        result += 9;
                        i++;
                    }
                    else
                    {
                        result += numbers[s[i]];
                    }
                    break;
                case 'X':
                    if (s[i + 1] == 'L')
                    {
                        result += 40;
                        i++;
                    }
                    else if (s[i + 1] == 'C')
                    {
                        result += 90;
                        i++;
                    }
                    else
                    {
                        result += numbers[s[i]];
                    }
                    break;
                case 'C':
                    if (s[i + 1] == 'D')
                    {
                        result += 400;
                        i++;
                    }
                    else if (s[i + 1] == 'M')
                    {
                        result += 900;
                        i++;
                    }
                    else
                    {
                        result += numbers[s[i]];
                    }
                    break;
                default:
                    result += numbers[s[i]];
                    break;
            }
            i++;
        }

        return result;
    }
}