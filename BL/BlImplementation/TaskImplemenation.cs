﻿using BlApi;
using BO;
//using DalApi;
using DO;
using System.Data.Common;
using System.Linq;

namespace BlImplementation;

internal class TaskImplemenation :ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Adding a new task for the list
    /// </summary>
    /// <param name="item">The item that is needed to be added to the list</param>
    /// <returns>The function returns the id of the new task</returns>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public int Create(BO.Task item)
    {
        if (item.Id < 0 || item.Name == "")
            throw new BlWrongInput("wrong input");
        DO.Task doTask = new DO.Task
              (item.Name,item.Description,item.Id, " ",(DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1,item.CreatedAtDate,item.RequiredEffortTime,false,item.DeadlineDate,item.ScheduledDate,item.StartDate,item.CompleteDate,item.Remarks);
        try
        {
            foreach( TaskInList dependency in item.Dependencies!) { _dal.Dependency.Create(new DO.Dependency(0, item.Id, dependency.Id)); }//add the list of dependecis in the bo task to the dependecies list in the data source in the dal
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Task with ID={item.Id} already exists", ex);
        }
    }
    /// <summary>
    /// The function deletes an existing task from the list 
    /// </summary>
    /// <param name="id">Searching the task to delete by it's id</param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BlCanNotDelete"></exception>
    public void Delete(int id)
    {
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!;
        IEnumerable<DO.Dependency>? taskDependent=from dependent in dependentList//a collection of all the dependecies with the tasks that depencence on this task
                                                  where dependent.DependentOnTask==id
                                                  select dependent;
        if (taskDependent.Any())//the collection is not null-there are tasks the dependence on this task
            throw new BlCanNotDelete($"Task with ID {id} can't be deleted");
        try
        {
            foreach (Dependency dependency in dependentList)
            {
                if (dependency.DependentTask==doTask.Id)
                {
                    _dal.Dependency.Delete(dependency.Id);
                }
            }
            _dal.Task.Delete(id);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }
    /// <summary>
    /// The function searches for a task with a requested id and returns the task if it actually in the list
    /// </summary>
    /// <param name="id">the requested id</param>
    /// <returns>Returns the task if it's in the list and if the task is not in the list, the function returns null</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {
            Id = id,
            Name = doTask.Name,
            Description = doTask.Descriptoin,
            Copmlexity = (BO.EngineerLevel)doTask.Complexity,
            EngineerTask = calculateEngineerTask(doTask.Engineerid),
            CreatedAtDate = doTask.CreateDate,
            RequiredEffortTime = doTask.RiquiredEffortTime,
            ForecastDate = doTask.OptionalDeadline,
            StartDate = doTask.StartDate,
            DeadlineDate = doTask.ActualDeadline,
            Remarks = doTask.Note,
            Status = GetStatus(doTask),
            Dependencies= GetAllDependencys(doTask)
        };

    }
    /// <summary>
    /// The function needs to return a copy of the list with all the tasks
    /// </summary>
    /// <returns>Returns the list of tasks</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();
        IEnumerable<BO.Task> BOTaskList =
        from item in TaskList
        orderby item.Id
        select new BO.Task
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Descriptoin,
            Copmlexity = (BO.EngineerLevel)item.Complexity,
            EngineerTask = calculateEngineerTask(item.Engineerid),
            CreatedAtDate = item.CreateDate,
            RequiredEffortTime = item.RiquiredEffortTime,
            ForecastDate = item.OptionalDeadline,
            StartDate = item.StartDate,
            DeadlineDate = item.ActualDeadline,
            Remarks = item.Note,
            Status = GetStatus(item),
            Dependencies = GetAllDependencys(item)
        };
        if (filter != null)
        { return BOTaskList.Where(filter); }

        else { return BOTaskList; }
    }
    /// <summary>
    /// The function updates details of an existing task and swich it with the old task and recognizes the requested task by it's id
    /// </summary>
    /// <param name="item">A new item that contains the task with the old details if it's actually was found in the list</param>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task item)
    {
        if (item.Id < 0 || item.Name == "")
            throw new BlWrongInput("wrong input");
        if ((_dal.Task.Read(item.Id)!.StartDate!=null)&&(_dal.Task.Read(item.Id)!.StartDate!= item.StartDate))
        {
            try { UpdateStartDate(item.Id, item.StartDate); }
            catch (BO.BlcanotUpdateStartdate ex)
            { throw new BO.BlcanotUpdateStartdate($"Can not update the date", ex); };
        }
        DO.Task doTask = new DO.Task
              (item.Name, item.Description, item.Id, " ", (DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1, item.CreatedAtDate, item.RequiredEffortTime, false, item.DeadlineDate, item.ScheduledDate, item.StartDate, item.CompleteDate, item.Remarks);
       try
        {
          // IEnumerable<DO.Dependency>? oldDependency = _dal.Dependency.ReadAll().Where(item1 => item1!.DependentTask == item.Id)!;
           // foreach (DO.Dependency dep in oldDependency) { _dal.Dependency.Delete(dep.Id); }
           // foreach (TaskInList dependency in item.Dependencies!)
           // {
            //    _dal.Dependency.Create(new DO.Dependency(FindDependent(item.Id, dependency.Id), item.Id, dependency.Id));

          //  }
            _dal.Task.Update(doTask);

    }
        catch (DO.DalDoesNotExistException ex) 
        {
            throw new BO.BlDoesNotExistException($"Task with ID={item.Id} does Not exist", ex);
        }
    }
    /// <summary>
    /// update the start date
    /// </summary>
    /// <param name="id">id of a task</param>
    /// <param name="startDate">the date we want to put</param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlcanotUpdateStartdate"></exception>
    /// <exception cref="BO.BlTooEarlyDate"></exception>
    public void UpdateStartDate(int id, DateTime? startDate)
    {
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!.Where(item => item!.DependentTask! == id)!;//a collection of all the dependencies with this task
        IEnumerable<DateTime?>? taskDependent = from dependent in dependentList
                                                let item = _dal.Task.Read(dependent.DependentOnTask)
                                                where (item != null) && (item.StartDate == null)
                                                select item.StartDate;
        if (taskDependent != null)//there ia a task that this task dependent on that didnt start yet
            throw new BO.BlcanotUpdateStartdate($"Can not update the date becuse task with {id} dependent on other task that didnt start yet ");
        taskDependent = from dependent in dependentList
                        let item = _dal.Task.Read(dependent.DependentOnTask)
                        where (item != null) && (item.StartDate+item.RiquiredEffortTime > startDate)
                        select item.StartDate;
        if (taskDependent != null)//there ia a task that this task dependent on that finish after the new start date
            throw new BO.BlTooEarlyDate($"The date {startDate} is too early");
        
        DO.Task updateTask = new DO.Task
         (doTask.Name, doTask.Descriptoin, doTask.Id, doTask.Product, doTask.Complexity, doTask.Engineerid, doTask.CreateDate, doTask.RiquiredEffortTime, false, doTask.OptionalDeadline, startDate, doTask.StartTaskDate, doTask.ActualDeadline, doTask.Note);
        try
        {
            _dal.Task.Update(updateTask);

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={updateTask.Id} does Not exist", ex);
        }
    }

    /// <summary>
    /// return the engineer that assign to the task
    /// </summary>
    /// <param name="id">id of the engineer</param>
    /// <returns></returns>
    public Tuple<int?,string>? calculateEngineerTask(int? id)
    {
        if (id == null) return null;
        DO.Engineer engineerName = _dal.Engineer.ReadAll().FirstOrDefault(item => item!.Id == id)!;
        string name = engineerName.Name;
        
        return new Tuple<int?, string>(id, name);
    }
    /// <summary>
    /// assign an engineer to the task
    /// </summary>
    /// <param name="idEngineer">id of the engineer</param>
    /// <param name="idTask">id of the task</param>
    /// <exception cref="BlNotAtTheRightStageException"></exception>
    /// <exception cref="BlCanNotAssignRequestedEngineer"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void updateEngineerToTask (int idEngineer, int idTask)
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Doing) throw new BlNotAtTheRightStageException("can't assign engineer to the task at the current stage of the project");
        //only in stage of doing we can assign engineer to a task
        DO.Engineer? engineer = _dal.Engineer.Read(idEngineer);
        DO.Task? task = _dal.Task.Read(idTask);
        if ((task!=null)&&(engineer!=null)&&(task.Complexity<=engineer.Complexity))
        {
            IEnumerable<DO.Task> taskList=
                from  DO.Task item in _dal.Task.ReadAll()
                where item.Engineerid == idEngineer//the engineer is already assigned to another task
                select item;
            if (taskList != null) throw new BlCanNotAssignRequestedEngineer("the engineer you want to assingn is alreade assigned to other task");
            IEnumerable<DO.Task> taskDependencys =
              from DO.Dependency item in _dal.Dependency.ReadAll()
              where item.DependentTask == idTask//the task dependent on other task
              select _dal.Task.Read(item.DependentOnTask);
            taskDependencys.Where(item => (item.StartDate + item.RiquiredEffortTime) !< DateTime.Today);//the task's that this task dependent on finish day, is before today
            if (taskDependencys!=null) throw new BlCanNotAssignRequestedEngineer("the engineer you want to assingn can't be assigned to the riquested task");
            DO.Task taskToUpdate = new DO.Task(task.Name, task.Descriptoin, task.Id, task.Product, task.Complexity, idEngineer, task.CreateDate, task.RiquiredEffortTime, false, task.OptionalDeadline, task.StartDate, task.StartTaskDate, task.ActualDeadline, task.Note);
            try { _dal.Task.Update(taskToUpdate);
            }
           
             catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={idTask} does Not exist", ex);
            }
        }

        }
    /// <summary>
    /// return the status of the task
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
public BO.Status GetStatus( DO.Task task)
{
        if (task.StartDate == null)
            return BO.Status.Unschedeled;
        else if ((task.StartDate != null) && (task.StartDate > DateTime.Today))
            return BO.Status.Schedeled;
        else if ((task.StartDate < DateTime.Today)&&(task.StartDate+task.RiquiredEffortTime > DateTime.Today))
            return BO.Status.OnTrack;
        else //if (task.StartDate + task.RiquiredEffortTime < DateTime.Today)
            return BO.Status.Done;
}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public List<TaskInList>? GetAllDependencys(DO.Task task)
    {
        IEnumerable<DO.Task> taskDependencys =
             from DO.Dependency item in _dal.Dependency.ReadAll()
             where item.DependentTask == task.Id
             select _dal.Task.Read(item.DependentOnTask);
        List<TaskInList>? dependencys=new List<TaskInList>();
        foreach (var taskDependency in taskDependencys)
        {
            TaskInList task1 = new TaskInList
            {
                Id = taskDependency.Id,
                Description = taskDependency.Descriptoin,
                Name = taskDependency.Name,
                Status = GetStatus(taskDependency)
            };
            dependencys.Add(task1);
        }
        return dependencys;
    }
    public  int FindDependent(int idDependency, int idDependentOn)
    {
        foreach (DO.Dependency? dependency in _dal.Dependency.ReadAll())
        { 
            if ((dependency != null) && (dependency.DependentTask == idDependency) && (dependency.DependentOnTask == idDependentOn))
                return dependency.Id;
        };
        return 0;
    }
    public  void clear()
    {
        //s_dal.Engineer.clear();
        //_dal.Task.clear();
        _dal.Dependency.clear();
        _dal.Task.clear();
        
    }
}


