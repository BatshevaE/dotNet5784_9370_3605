
namespace BO;
public class Task
{
    public int Id { get; init; }

   public string Name { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAtDate { get; init; }//Date when the task was added to the system
    public Status Status { get; set; }  //calculated

    public List<BO.TaskInList>? Dependencies { get; set; }//relevant only before schedule is built']

    public TimeSpan RequiredEffortTime { get; set; }    //how many men-days needed for the task (for MS it is null)
    public DateTime? StartDate{ get; set; }//the real start date
    public DateTime? ScheduledDate { get; set; }//the planned start date
    public DateTime? ForecastDate { get; set; }//calcualed planned completion date']
    public DateTime? DeadlineDate { get; set; }//the latest complete date
    public DateTime? CompleteDate { get; set; }//task: real completion date']
    
    public string? Remarks { get; set; }//free remarks from project meetings']
    public EngineerLevel Copmlexity { get; set; }    //task: minimum expirience for engineer to assign
    public  Tuple<int?, string>? EngineerTask  { get; set; }

    public override string ToString() => this.ToStringProperty();

}

