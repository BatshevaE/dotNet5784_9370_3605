namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public ITask Task =>  new TaskImplemenation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public ITaskInList TaskInList =>  new TaskInListImplemenation();

    public IEngineerInTask EngineerInTask =>  new EngineerInTaskImplemenation();

    //private DalApi.IDal _dal = DalApi.Factory.Get;

    //public void setStartProject(DateTime startProject)
    //{
    //    DalApi.Factory.Get.setStartProject(startProject);
    //}
}

