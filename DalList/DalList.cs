
namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IUser User => new UserImplementation();
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public DateTime? StartProject
    { 
        get { return DataSource.Config.startWorkProject; } 
        set { DataSource.Config.startWorkProject = value; } 
    }
    public DateTime? EndProject
    {
        get { return DataSource.Config.endWorkProject; }
        set { DataSource.Config.endWorkProject = value; }
    }

}

