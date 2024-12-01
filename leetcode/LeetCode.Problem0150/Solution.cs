public class Solution
{
    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if (int.TryParse(tokens[i], out int num))
            {
                stack.Push(num);
            }
            else
            {
                int num1 = stack.Pop();
                int num2 = stack.Pop();
                switch (tokens[i])
                {
                    case "+":
                        stack.Push(num2 + num1);
                        break;
                    case "-":
                        stack.Push(num2 - num1);
                        break;
                    case "*":
                        stack.Push(num2 * num1);
                        break;
                    case "/":
                        stack.Push(num2 / num1);
                        break;
                }
            }
        }

        return stack.Pop();
    }
}