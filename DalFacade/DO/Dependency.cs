namespace DO;
/// <summary>
/// The record Dependency represents a the dependency of task in other tasks during the project
/// </summary>
/// <param name="Id">The id that helps to recognize the task</param>
/// <param name="DependentTask">The dependent task</param>
/// <param name="DependentOnTask">The task that needs to be done so the dependent task would be able to be done too</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependentOnTask
)
{
    /// <summary>
    /// An empty ctor
    /// </summary>
    public Dependency() : this(0, 0, 0) { }
    //We chose to write the record in the second way which the parameters ctor is already exists
}
