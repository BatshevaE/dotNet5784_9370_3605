using DalApi;

namespace BlApi;
public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInTask EngineerInTask { get; }
}
