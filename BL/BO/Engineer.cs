using System.Globalization;

namespace BO;

public class Engineer
{
    /// <summary>
    /// the Engineer id
    /// </summary>
    int Id { get; init; }
    /// <summary>
    /// the Engineer name
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// the Engineer mail
    /// </summary>
    string Email { get; set; }
    /// <summary>
    /// the Engineer complex
    /// </summary>
    EngineerLevel Level { get; set; }
    /// <summary>
    /// the Engineer cost for an hour
    /// </summary>
    double Cost { get; set; }
    /// <summary>
    /// a task that the Engineer is now doing,include name and id of the task
    /// </summary>
    Tuple<int,string>? Task { get; set; }
    public override string ToString() => this.ToStringProperty();

}
