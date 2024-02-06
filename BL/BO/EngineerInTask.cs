namespace BO;

public class EngineerInTask
{
    int TaskId { get; init; }    
    string? TaskName { get; set; }
    
    public override string ToString() => this.ToStringProperty();

}
