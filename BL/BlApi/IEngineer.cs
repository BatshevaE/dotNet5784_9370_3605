
namespace BlApi;

//intarface that contains all definitions of the functions of EngineerImpliminations
public interface IEngineer
{
    /// <summary>
    ////create a new enginner
    /// </summary>
    /// <param name="item">the task we want to create</param>
    /// <returns></returns>
    public int Create(BO.Engineer item);
    /// <summary>
    ///returns an engineer with id or null
    /// </summary>
    /// <param name="id">id of task to read</param>
    /// <returns></returns>
    public BO.Engineer? Read(int id);
    /// <summary>
    ///returns all engineers
    /// </summary>
    /// <param name="filter">if we want all engineers in some sort</param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    /// <summary>
    ///update an engineer the engineer to update
    /// </summary>
    /// <param name="item"></param>
    public void Update(BO.Engineer item);
    /// <summary>
    ///deletes engineer with id
    /// </summary>
    /// <param name="id">id of engineer to delete</param>
    public void Delete(int id);
    /// <summary>
    ///returns list of tuples that contains id and name of tasks that the engineer is assigned to
    /// </summary>
    /// <param name="id">id of the engineer to assign</param>
    /// <returns></returns>
    public List<Tuple<int, string>?>? CalculateTaskInEngineer(int id);
    /// <summary>
    ///deletes all data of engineers 
    /// </summary>
    public void Clear();
    /// <summary>
    ///collection of engineers in a requested level
    /// </summary>
    /// <param name="level">the requested level</param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer>? EngineersAtRequestedLevel(BO.EngineerLevel level);



}
