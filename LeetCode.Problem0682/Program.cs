public class Solution
{
    public int CalPoints(string[] operations)
    {
        var list = new List<int>(operations.Length);

        int sum = 0;

        foreach (var operation in operations)
        {
            switch (operation)
            {
                case "+":
                    var result = list[list.Count - 1] + list[list.Count - 2];
                    list.Add(result);
                    sum += result;
                    break;
                case "D":
                    var product = list[list.Count - 1] * 2;
                    list.Add(product);
                    sum += product;
                    break;
                case "C":
                    sum -= list[list.Count - 1];
                    list.RemoveAt(list.Count - 1);
                    break;
                default:
                    var value = int.Parse(operation);
                    list.Add(value);
                    sum += value;
                    break;
            }
        }

        return sum;
    }
}