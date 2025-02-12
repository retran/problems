public class Solution
{
    public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies)
    {
        var keyToIdMap = new Dictionary<string, int>();
        var idToKeyMap = new Dictionary<int, string>();
        var recipesSet = new HashSet<int>();
        var queue = new Queue<int>();

        int count = 0;

        foreach (var recipe in recipes)
        {
            keyToIdMap[recipe] = count;
            idToKeyMap[count] = recipe;
            recipesSet.Add(count);
            count++;
        }

        foreach (var supply in supplies)
        {
            keyToIdMap[supply] = count;
            idToKeyMap[count] = supply;
            queue.Enqueue(count);
            count++;
        }

        var graph = new HashSet<int>[count];
        var inDegrees = new int[count];

        for (int i = 0; i < count; i++)
        {
            graph[i] = new HashSet<int>();
        }

        for (int recipe = 0; recipe < ingredients.Count; recipe++)
        {
            foreach (var ingredient in ingredients[recipe])
            {
                if (keyToIdMap.TryGetValue(ingredient, out var id))
                {
                    graph[id].Add(recipe);
                }
                inDegrees[recipe]++;
            }
        }

        var reachableRecipes = new HashSet<int>();

        while (queue.Count > 0)
        {
            var id = queue.Dequeue();

            if (recipesSet.Contains(id))
            {
                reachableRecipes.Add(id);
            }

            foreach (var next in graph[id])
            {
                inDegrees[next]--;
                if (inDegrees[next] == 0)
                {
                    queue.Enqueue(next);
                }
            }
        }

        return reachableRecipes
            .Select(id => idToKeyMap[id])
            .ToList();
    }
}