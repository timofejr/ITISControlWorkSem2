namespace ConsoleApp1;

[PluginDependency]
public class GameCorePlugin: IPlugin
{
    public string Name { get; set; }
    public void Download()
    {
        Task.Delay(new Random().Next(500, 1000)).Wait();
    }
}