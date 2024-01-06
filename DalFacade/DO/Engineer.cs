namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id">the id of the engineer</param>
/// <param name="Name">the engineer's name</param>
/// <param name="EmailAsress">the engineer's email adress</param>
/// <param name="Complexity">The complexity level of the engineer </param>
/// <param name="CostForHour">how much monry does the engineer get</param>
public record Engineer
    (
    int Id,
    string?  Name,
    string? EmailAsress,
    EngineerLevel? Complexity,
    double? CostForHour
    )
{
    /// <summary>
    /// An empty ctor
    /// </summary>
    public Engineer() : this(0, "", "",0,0.0) { }
    //We chose to write the record in the second way which the parameters ctor is already exists
}
