namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";
    public int Create(Task item)
    {
        List<Task> Tasks=XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task newTask = item with { Id =Config.NextTaskId };
        Tasks.Add(newTask);
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);
        return newTask.Id;
    }

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

    public Task? Read(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return Tasks.FirstOrDefault(Task => Task.Id == id);//stage 2 

    }

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
}
