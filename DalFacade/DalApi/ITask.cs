namespace DalApi;
using DO;

public interface ITask
{
    /// <summary>
    /// Creates new entity of task in DAL
    /// </summary>
    /// <param name="item">Gets a task to add to the list</param>
    /// <returns>The id of the new task</returns>
    int Create(Task item);
    /// <summary>
    /// Reads entity of task by its ID 
    /// </summary>
    /// <param name="id">Gets an id of a task to print</param>
    /// <returns>return the task to read</returns>
    Task? Read(int id);
    /// <summary>
    /// stage 1 only, Reads all entity of tasks
    /// </summary>
    /// <returns>return the list of tasks</returns>
    List<Task> ReadAll();
    /// <summary>
    /// Updates entity of task
    /// </summary>
    /// <param name="item">Gets a task to update</param>
    void Update(Task item);
    /// <summary>
    /// Deletes a task by its Id
    /// </summary>
    /// <param name="id">Gets an id of a task to delete</param>
    void Delete(int id);

}
