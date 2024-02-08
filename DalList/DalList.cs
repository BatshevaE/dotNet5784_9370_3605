
namespace Dal;
using DalApi;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public DateTime? StartProject
    { 
        get { return DataSource.Config.startWorkProject; } 
        set { DataSource.Config.startWorkProject = value; } 
    }
    //public DateTime? StartProject { get; set; }
    //public void setStartProject(DateTime startProject)
    //{
    //    DataSource.Config.startWorkProject = startProject;
    //}
    
}

