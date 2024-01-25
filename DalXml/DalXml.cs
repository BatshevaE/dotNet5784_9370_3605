namespace Dal;
using DalApi;
using System.Diagnostics;

/// <summary>
/// interface of the entities in the xml layer
/// </summary>
sealed  internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

}
