namespace ConsoleApp1;

public class Graph
{
    private readonly Dictionary<Node, List<Node>> _edges = new ();

    public void AddNode(Type data)
    {
        var node = new Node(data);
        
        _edges.Add(node, new List<Node>());
    }

    public Node? GetNode(Type data)
    {
        return _edges.Keys.First(x => x.Data == data);
    }

    public void Connect(Node n1, Node n2)
    {
        _edges[n1].Add(n2);
    }

    public List<List<Type>> GetTopologicallySortedSequence()
    {
        Dictionary<Node, int> degrees = new();

        foreach (var node in _edges)
        {
            degrees.TryAdd(node.Key, 0);

            foreach (var node2 in node.Value)
            {
                if (!degrees.ContainsKey(node2))
                {
                    degrees.Add(node2, 0);
                }

                degrees[node2]++;
            }
        }
        
        int visitedCount = 0;
        Queue<Node> queue = new ();
        List<List<Type>> levels = new();

        foreach (var degree in degrees)
        {
            if (degree.Value == 0)
                queue.Enqueue(degree.Key);
        }
        
        while (queue.Count > 0)
        {
            int count = queue.Count;
            List<Type> currentLevel = new();

            for (int i = 0; i < count; i++)
            {
                var current = queue.Dequeue();
                currentLevel.Add(current.Data);
                visitedCount++;

                if (_edges.TryGetValue(current, out var neighbors))
                {
                    foreach (var neighbor in neighbors)
                    {
                        degrees[neighbor]--;
                        if (degrees[neighbor] == 0)
                            queue.Enqueue(neighbor);
                    }
                }
            }

            levels.Add(currentLevel);
        }
        
        if (visitedCount != _edges.Count)
            throw new Exception("Graph contains cycles");

        return levels;
    }

    public IEnumerable<Node> Nodes
    {
        get
        {
            foreach (var node in _edges.Keys)
                yield return node;
        }
    }

    public Dictionary<Node, List<Node>> Edges
    {
        get => _edges; 
    }
}