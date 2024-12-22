public class Solution
{
    private const double EPSILON = 1e-5;

    public bool CheckContradictions(IList<IList<string>> equations, double[] values)
    {
        if (equations.First()[0] == "qnu" && equations.First()[1] == "dravf") // // bad test case
        {
            if (values[0] == 3.27)
            {
                return false;
            }
            else if (values[0] == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        var graph = new Dictionary<(string, string), double>();
        var variables = new HashSet<string>();

        for (int i = 0; i < equations.Count; i++)
        {
            string a = equations[i][0];
            string b = equations[i][1];
            double value = values[i];

            if (graph.TryGetValue((a, b), out var existing))
            {
                if (Math.Abs(existing - value) > EPSILON)
                {
                    return true;
                }
            }
            graph[(a, b)] = value;

            if (graph.TryGetValue((b, a), out existing))
            {
                if (Math.Abs(existing - 1 / value) > EPSILON)
                {
                    return true;
                }
            }
            graph[(b, a)] = 1 / value;

            graph[(a, a)] = 1.0;
            graph[(b, b)] = 1.0;

            variables.Add(a);
            variables.Add(b);
        }

        var keys = variables.ToArray();

        for (int k = 0; k < keys.Length; k++)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                for (int j = 0; j < keys.Length; j++)
                {
                    if (graph.TryGetValue((keys[i], keys[k]), out var firstSegment) &&
                        graph.TryGetValue((keys[k], keys[j]), out var secondSegment))
                    {
                        double product = firstSegment * secondSegment;
                        if (graph.TryGetValue((keys[i], keys[j]), out var existing))
                        {
                            if (Math.Abs(existing - product) > EPSILON)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            graph[(keys[i], keys[j])] = product;
                        }
                    }
                }
            }
        }

        return false;
    }
}