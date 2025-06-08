namespace ConsoleApp1;

[PluginDependency(typeof(GameCorePlugin))]
public class AssetLoaderPlugin: IPlugin
{
    public string Name { get; }
    public void Download()
    {
        Task.Delay(new Random().Next(500, 1000)).Wait();
    }
}