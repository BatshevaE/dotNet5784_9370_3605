using System.Reflection.Emit;

namespace BO;

public class TaskInList
{
    /// <summary>
    /// id of the task
    /// </summary>
   public int Id { get; init; }
    /// <summary>
    /// complexity
    /// </summary>
    public EngineerLevel Copmlexity {  get; init; }
    /// <summary>
    /// description
    /// </summary>
   public  string? Description { get; set; }
    /// <summary>
    /// name
    /// </summary>
   public string? Name { get; set; }
    /// <summary>
    /// status-dependns on the actual start date
    /// </summary>
   public  Status Status { get; set; }
    public override string ToString() => this.ToStringProperty();
    /// <summary>
    /// empty ctor
    /// </summary>
    public TaskInList() { Id = 0; Name = ""; Copmlexity = 0; Description = ""; Status = 0;  }
}
