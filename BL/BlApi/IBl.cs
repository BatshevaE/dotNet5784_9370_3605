using DalApi;
//intarface that contains all definitions of the functions of BL 
namespace BlApi;
public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInTask EngineerInTask { get; }
    public IUser User { get; }
    public DateTime Clock { get; }
    public DateTime AddDay();
    public DateTime AddMonth(); 
    public DateTime AddYear();  
    public DateTime InitializeClock();
}
