namespace BlImplementation;
using BlApi;
using Dal;

internal class Bl : IBl
{
    public IUser User => new UserImplementation();
    public ITask Task =>  new TaskImplemenation(this);

    public IEngineer Engineer =>  new EngineerImplementation();

    public ITaskInList TaskInList =>  new TaskInListImplemenation();

    public IEngineerInTask EngineerInTask =>  new EngineerInTaskImplemenation();

    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }
    public DateTime AddDay() { Clock = Clock.AddDays(1); return Clock; }
    public DateTime AddMonth() {  Clock = Clock.AddMonths(1); return Clock; }
    public DateTime AddYear() {  Clock = Clock.AddYears(1);return Clock; }
    public DateTime InitializeClock() { Clock=DateTime.Now; return Clock; }
}

