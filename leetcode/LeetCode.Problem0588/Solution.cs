using System;
using System.Collections.Generic;

public class FileSystem
{
    private class Entry
    {
        public bool IsFile = false;
        public string Content = "";
        public SortedDictionary<string, Entry> Children = new();
    }

    private readonly Entry _root;

    public FileSystem()
    {
        _root = new Entry();
    }

    public IList<string> Ls(string path)
    {
        Entry node = TraversePath(path);

        IList<string> result = new List<string>();

        if (node.IsFile)
        {
            // If the node is a file, return just the file name
            string[] components = path.Split('/');
            result.Add(components[components.Length - 1]);
        }
        else
        {
            // If the node is a directory, return all child names (sorted by default)
            foreach (var key in node.Children.Keys)
            {
                result.Add(key);
            }
        }
        return result;
    }

    public void Mkdir(string path)
    {
        TraversePath(path, createIfMissing: true);
    }

    public void AddContentToFile(string filePath, string content)
    {
        Entry node = TraversePath(filePath, createIfMissing: true);
        node.IsFile = true;
        node.Content += content;
    }

    public string ReadContentFromFile(string filePath)
    {
        Entry node = TraversePath(filePath);
        return node.Content;
    }

    private Entry TraversePath(string path, bool createIfMissing = false)
    {
        string[] components = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        Entry node = _root;

        foreach (string component in components)
        {
            if (!node.Children.ContainsKey(component))
            {
                if (createIfMissing)
                {
                    node.Children[component] = new Entry();
                }
                else
                {
                    throw new Exception("Path does not exist");
                }
            }
            node = node.Children[component];
        }

        return node;
    }
}
