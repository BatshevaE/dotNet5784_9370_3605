namespace BO;

public class Engineer
{
    /// <summary>
    /// the Engineer id
    /// </summary>
   public int Id { get; init; }
    /// <summary>
    /// the Engineer name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// the Engineer mail
    /// </summary>
     public string Email { get; set; }
    /// <summary>
    /// the Engineer complex
    /// </summary>
   public EngineerLevel Level { get; set; }
    /// <summary>
    /// the Engineer cost for an hour
    /// </summary>
    public double Cost { get; set; }
    /// <summary>
    /// a task that the Engineer is now doing,include name and id of the task
    /// </summary>
    public Tuple<int,string>? Task { get; set; }
    public override string ToString() => this.ToStringProperty();

}
