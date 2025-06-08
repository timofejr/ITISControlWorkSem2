using System.Diagnostics;

namespace ConsoleApp1;

public interface IPlugin
{
    string Name { get; }
    void Download();
}