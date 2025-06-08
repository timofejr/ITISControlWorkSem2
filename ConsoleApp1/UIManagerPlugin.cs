namespace ConsoleApp1;

[PluginDependency(typeof(AssetLoaderPlugin), typeof(InputHandlerPlugin))]
public class UIManagerPlugin: IPlugin
{
    public string Name { get; }
    public void Download()
    {
        Task.Delay(new Random().Next(500, 1000)).Wait();
    }
}