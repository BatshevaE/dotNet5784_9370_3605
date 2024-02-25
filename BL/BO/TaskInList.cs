using System.Reflection.Emit;

namespace BO;

public class TaskInList
{
   public int Id { get; init; }
    public EngineerLevel Copmlexity {  get; init; }
   public  string? Description { get; set; }
   public string? Name { get; set; }
   public  Status Status { get; set; }
    public override string ToString() => this.ToStringProperty();
    public TaskInList() { Id = 0; Name = ""; Copmlexity = 0; Description = ""; Status = 0;  }
}
