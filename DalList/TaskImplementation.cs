namespace Dal;
using DalApi;
using DO;

/// <summary>
/// The CRUB functions for Task
/// </summary>
public class TaskImplementation : ITask
{
    /// <summary>
    /// Adding a new task fir the list
    /// </summary>
    /// <param name="item">The item that is needed to be added to the list</param>
    /// <returns>The function returns the id of the new task</returns>
    public int Create(Task item)
    {

        Task newTask = item with { Id = DataSource.Config.NextTaskId };
        DataSource.Tasks.Add(newTask);
        return newTask.Id;
    }
    /// <summary>
    /// The function deletes an existing task from the list 
    /// </summary>
    /// <param name="id">Searching the task to delete by it's id</param>
    /// <exception cref="Exception">If the requested task is not in the list an exception is thrown</exception>
    public void Delete(int id)
    {
        Task? task = DataSource.Tasks.Find(Task => Task.Id == id);
        if (task != null)
        {
            DataSource.Tasks.Remove(task);
        }
        else
            throw new Exception($"Task with ID={id} does Not exist");
    }
    /// <summary>
    /// The function searches for a task with a requested id and returns the task if it actually in the list
    /// </summary>
    /// <param name="id">the requested id</param>
    /// <returns>Returns the task if it's in the list and if the task is not in the list, the function returns null</returns>
    public Task? Read(int id)
    {
       return(DataSource.Tasks.Find(Task => Task.Id == id));  
    }
    /// <summary>
    /// The function needs to return a copy of the list with all the tasks
    /// </summary>
    /// <returns>Returns the list of tasks</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }
    /// <summary>
    /// The function updates details of an existing task and swich it with the old task and recognizes the requested task by it's id
    /// </summary>
    /// <param name="item">A new item that contains the task with the old details if it's actually was found in the list</param>
    /// <exception cref="Exception">Throws that the requested task is not in the list if a task with the requested id wasn't found</exception>
    public void Update(Task item)
    {
       
            Task? task = DataSource.Tasks.Find(Task => Task.Id == item.Id);
            if (task != null)
            {
                DataSource.Tasks.Remove(task);
                DataSource.Tasks.Add(item);
            }
            else
                throw new Exception($"Task with ID={item.Id} does Not exist");
        }
    }


