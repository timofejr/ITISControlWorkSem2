namespace ConsoleApp1;

public class PluginDependencyAttribute: Attribute
{
    public HashSet<Type> Dependencies { get; }

    public PluginDependencyAttribute(params Type[] dependencies)
    {
        Dependencies = new HashSet<Type>(dependencies);
    } 
}