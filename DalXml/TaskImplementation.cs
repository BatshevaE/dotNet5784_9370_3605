namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
/// <summary>
/// implementation of task to the xml file
/// </summary>
internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";
    /// <summary>
    /// creates a tast and write it in the xml file
    /// </summary>
    /// <param name="item">the task that needed to be created</param>
    /// <returns></returns>
    public int Create(Task item)
    {
        List<Task> Tasks=XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task newTask = item with { Id =Config.NextTaskId };
        Tasks.Add(newTask);
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);
        return newTask.Id;
    }
    /// <summary>
    /// deletes a task from the list and updates the xml file 
    /// </summary>
    /// <param name="id">the id of the deleted task</param>
    /// <exception cref="DalDoesNotExistException"></exception>

    public void Delete(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task? task = Tasks.Find(Task => Task.Id == id);
        if (task != null)
        {
           Tasks.Remove(task);
        }
        else
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

    }
    /// <summary>
    /// reads a task from the xml file
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return Tasks.FirstOrDefault(Task => Task.Id == id);//stage 2 

    }
    /// <summary>
    /// reads from the xml file the first task that stands the condition
    /// </summary>
    /// <param name="filter">the requested condition</param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool>? filter)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        if (filter == null)
        {
            //XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);
            return null;
        }
        else
        {
            //XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

            return Tasks.FirstOrDefault(filter);
        }
       

    }
    /// <summary>
    /// reads all the tasks frim the xml file under a condition
    /// </summary>
    /// <param name="filter">the requested condition</param>
    /// <returns></returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        if (filter != null)
        {
            return from item in Tasks
                   where filter(item)
                   select item;

        }
        return from item in Tasks
               select item;
    }
    /// <summary>
    /// updates a task in the xml file
    /// </summary>
    /// <param name="item">the updated task</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task? task = Tasks.Find(Task => Task.Id == item.Id);
        if (Tasks.FirstOrDefault(item) == null)
        {
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        }
        Tasks.Remove(task!);
        Tasks.Add(item);       
       XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);
        
    }
    /// <summary>
    /// deletes all the tasks from the xml file
    /// </summary>
   public void clear()
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Tasks.Clear();
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);
        Config.NextTaskId = 1;//initialize the running number
    }
}
