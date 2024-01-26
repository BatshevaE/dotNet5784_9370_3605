using DO;
using System;

namespace BO;
public class Task
{
    int Id { get; init; }

    string Name { get; set; }
    string Description { get; set; }

    DateTime CreatedAtDate { get; init; }//Date when the task was added to the system
    Status Status { get; set; }  //calculated
    
    List<BO.TaskInList>? Dependencies { get; set; }//relevant only before schedule is built']

    TimeSpan RequiredEffortTime { get; set; }    //how many men-days needed for the task (for MS it is null)
    DateTime StartDate{ get; set; }//the real start date
    DateTime ScheduledDate { get; set; }//the planned start date
    DateTime ForecastDate { get; set; }//calcualed planned completion date']
    DateTime DeadlineDate { get; set; }//the latest complete date
    DateTime CompleteDate { get; set; }//task: real completion date']
    
    string? Remarks { get; set; }//free remarks from project meetings']
    EngineerLevel Copmlexity { get; set; }    //task: minimum expirience for engineer to assign
    EngineerInTask? Engineer  { get; set; }

    public override string ToString() => this.ToStringProperty();

}

