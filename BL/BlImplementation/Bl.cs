namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public ITask Task =>  new TaskImplemenation();

    public IEngineer Engineer =>  new EngineerImplemented();

    public ITaskInList TaskInList =>  new TaskInListImplemented();

    public IEngineerInTask EngineerInTask =>  new EngineerInTaskImplemented();
}
