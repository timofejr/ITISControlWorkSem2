using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp1;

class Program
{
    static Graph graph = new ();

    static void FillGraph()
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        foreach (var type in assembly.GetTypes())
            if (type.IsClass && type.GetInterfaces().Contains(typeof(IPlugin)))
                graph.AddNode(type);

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsClass && type.GetInterfaces().Contains(typeof(IPlugin)))
            {
                var attribute = type.GetCustomAttribute<PluginDependencyAttribute>();
                var node1 = graph.GetNode(type);
                if (node1 == null) continue;
                
                foreach (var dependency in attribute.Dependencies)
                {
                    var node2 = graph.GetNode(dependency);
                    if (node2 == null) continue;
                    graph.Connect(node2, node1);
                }
            }
        }
    }

    static async Task DownloadPlugins(List<List<Type>> downloadingPlan)
    {
        for (int i = 0; i < downloadingPlan.Count; i++)
        {
            var tasks = new List<Task>();
            
            Console.WriteLine($"Параллельная заргрузка плагинов:");
            var stopwatch = Stopwatch.StartNew();
            foreach (var type in downloadingPlan[i])
            {
                Console.WriteLine($"  {type.Name}");
                var ctor = type.GetConstructor(Type.EmptyTypes);
                var plugin = (IPlugin)ctor.Invoke(null);
                tasks.Add(Task.Run(() => plugin.Download()));
            }
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"  Плагины загружены за : {stopwatch.Elapsed.Milliseconds}мс");
        }
    }
    
    static async Task Main(string[] args)
    { 
        // Составляем модель зависимостей и строим граф
        FillGraph();
        
        // Состовляем план загрузки
        var downloadingPlan = graph.GetTopologicallySortedSequence();
        
        // Загружаем плагины
        await DownloadPlugins(downloadingPlan);
    }
}