namespace ConsoleApp1;

[PluginDependency(typeof(PhysicsEnginePlugin), typeof(InputHandlerPlugin))]
public class AIControllerPlugin: IPlugin
{
    public string Name { get; }
    public void Download()
    {
        Task.Delay(new Random().Next(500, 1000)).Wait();
    }
}