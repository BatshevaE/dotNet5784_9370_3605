namespace BO;

public class TaskInList
{
   public int Id { get; init; }
   public  string Description { get; set; }
   public string Name { get; set; }
   public   Status Status { get; set; }
    public override string ToString() => this.ToStringProperty();

}
