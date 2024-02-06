
namespace BO;
public class Task
{
    /// <summary>
    /// id of task
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// name of task
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// description of task
    /// </summary>
    public string Description { get; set; }
    /// <summary>
     ///Date when the task was added to the system    /// </summary>
    public DateTime CreatedAtDate { get; init; }
    /// <summary>
    ////calculated 
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    ////relevant only before schedule is built']
    /// </summary>
    public List<BO.TaskInList>? Dependencies { get; set; }
    /// <summary>
    ////how many men-days needed for the task (for MS it is null)
    /// </summary>
    public TimeSpan RequiredEffortTime { get; set; }
    /// <summary>
    ////the real start date
    /// </summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    ////the planned start date
    /// </summary>
    public DateTime? ScheduledDate { get; set; }
    /// <summary>
    ////calcualed planned completion date']
    /// </summary>
    public DateTime? ForecastDate { get; set; }
    /// <summary>
    ////the latest complete date
    /// </summary>
    public DateTime? DeadlineDate { get; set; }
    /// <summary>
    ////task: real completion date']
    /// </summary>
    public DateTime? CompleteDate { get; set; }
    /// <summary>
    ////free remarks from project meetings']
    /// </summary>
    public string? Remarks { get; set; }
    /// <summary>
    // //task: minimum expirience for engineer to assign
    /// </summary>
    public EngineerLevel Copmlexity { get; set; }   
    /// <summary>
    /// engineer that assign to do the task
    /// </summary>
    public Tuple<int?, string>? EngineerTask { get; set; }
    /// <summary>
    /// empty ctor
    /// </summary>
    public Task() {
        Id = 0;
        Name = "";
        Description = "";
        CreatedAtDate = DateTime.Now;
        Status = 0;
        Dependencies = null;
        RequiredEffortTime = TimeSpan.Zero;
        StartDate = null;
        ScheduledDate = null;
        ForecastDate = null;
        DeadlineDate = null;
        CompleteDate = null;
        Remarks = null;
        Copmlexity = 0;
        EngineerTask = null;
    }
    public override string ToString() => this.ToStringProperty();

}





