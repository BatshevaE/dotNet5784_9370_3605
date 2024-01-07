namespace DO;
/// <summary>
/// 
/// </summary>
/// <summary>
/// The record Task represents a task with all it's details
/// </summary>
/// <param name="Name">The name of the task</param>
/// <param name="Descriptoin">Describes the goal of the task</param>
/// <param name="Id">Uniqe id that helps to recognize the task</param>
/// <param name="RiquiredEffortTime">The length of time that the task takes</param>
/// <param name="Product">Describes the final result of the task</param>
/// <param name="Complexity">The minimum level of engineer that can do the task</param>
/// <parm  name="Engineerid">The level of the engineer that was assigned to the task</parm> 
/// <param name="IsMileStone">In the Dal layer this field will be false and in the next levels it would be able to be changed to true</param>
/// <param name="OptionalDeadline">Optional maximum final date to the task</param>
/// <param name="CreateDate">The date of the creation of the task by the manager</param>
/// <param name="StartDate">The planned date to start the task</param>
/// <param name="StartTaskDate">The date when the engineer started working on the task</param>
/// <param name="ActualDeadline">When the engineer finished working on the task</param>
/// <param name="Note">Notes if the engineer about the task</param>
public record Task
(

    string? Name,
    string? Descriptoin,
    int Id,
    string Product,
    EngineerLevel Complexity,
    int Engineerid,
    TimeSpan RiquiredEffortTime=new TimeSpan(),
    bool? IsMileStone = false,
    DateTime? OptionalDeadline = null,
    DateTime? CreateDate = null,
    DateTime? StartDate = null,
    DateTime? StartTaskDate = null,
    DateTime? ActualDeadline = null,
    string? Note = null
)

{
    /// <summary>
    /// This is an empty ctor
    /// </summary>
    public Task() : this("", "", 0, "", 0, 0) { }
}
   //We chose to write the record in the second way which the parameters ctor is already exists
   