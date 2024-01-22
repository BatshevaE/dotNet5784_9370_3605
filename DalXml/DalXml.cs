namespace Dal;
using DalApi;
/// <summary>
/// interface of the entities in the xml layer
/// </summary>
sealed public class DalXml : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

}
