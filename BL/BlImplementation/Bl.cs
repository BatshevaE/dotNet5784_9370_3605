namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public ITask Task =>  new TaskImplemenation();

    public IEngineer Engineer =>  new EngineerImplemenation();

    public ITaskInList TaskInList =>  new TaskInListImplemenation();

    public IEngineerInTask EngineerInTask =>  new EngineerInTaskImplemenation();
}
