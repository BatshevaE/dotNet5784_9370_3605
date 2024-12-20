﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation;


internal class TaskImplemenation : BlApi.ITask
{
    /// <summary>
    /// the data source from the dal
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private readonly IBl _bl;
    internal TaskImplemenation(IBl bl) => _bl = bl;
    /// <summary>
    /// Adding a new task for the list
    /// </summary>
    /// <param name="item">The item that is needed to be added to the list</param>
    /// <returns>The function returns the id of the new task</returns>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public int Create(BO.Task item)
    {
        if (BlImplementation.Project.GetStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");//we can craete tasks only in the Planing Stage
        if (item.Id < 0 || item.Name == "")//check that the input is logically correct
            throw new BlWrongInput("wrong input");
        DO.Task doTask = new (item.Name, item.Description, item.Id, " ", (DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1, item.CreatedAtDate, item.RequiredEffortTime, false, item.DeadlineDate, item.ScheduledDate, item.StartDate, item.CompleteDate, item.Remarks);
        try
        {
            int idTask = _dal.Task.Create(doTask);//setd it to the lower layer to create the new task in the data source
            if(item.Dependencies!=null)//if the task dependns on other tasks
               item.Dependencies!.Select(dependency => _dal.Dependency.Create(new DO.Dependency(0, idTask, dependency.Id))).ToList();//create new dependencies in the dependency data source
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
        if (BlImplementation.Project.GetStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");//we can delete tasks only in the Planing Stage
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);//read the task to delete from the lower layer
        if (doTask == null)//if the task doesnt exist
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!;//collection of all the  dependencies
        IEnumerable<DO.Dependency>? taskDependent = (from dependent in dependentList//a collection of all the dependecies with the tasks that depencent on this task
                                                    where dependent.DependentOnTask == id
                                                    select dependent).ToList();
        if ((taskDependent.Any())&&(taskDependent.Count()!=0))//the collection is not null-there are tasks the dependence on this task
            throw new BlCanNotDelete($"Task with ID {id} can't be deleted");
        try
        {
             dependentList.Where(dependency => dependency.DependentTask == doTask.Id).Select(dependency => _dal.Dependency.Delete(dependency.Id)).ToList();//delete via the loxer layer from the data source
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
        DO.Task? doTask = _dal.Task.Read(id);//read the task from the lower layer
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {
            Id = id,
            Name = doTask.Name,
            Description = doTask.Descriptoin,
            Copmlexity = (BO.EngineerLevel)doTask.Complexity,
            EngineerTask = CalculateEngineerTask(doTask.Engineerid),
            CreatedAtDate = doTask.CreateDate,
            RequiredEffortTime = doTask.RiquiredEffortTime,
            ForecastDate = doTask.OptionalDeadline,
            StartDate = doTask.StartTaskDate,
            ScheduledDate=doTask.StartDate,
            DeadlineDate = doTask.ActualDeadline,
            Remarks = doTask.Note,
            Status = GetStatus(doTask),
            Dependencies = GetAllDependencys(doTask)
        };//returns a BO.Task of this task

    }
    /// <summary>
    /// The function needs to return a copy of the list with all the tasks of type BO.TaskInList
    /// </summary>
    /// <returns>Returns the list of tasks</returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();//a collection of all the tasks
        IEnumerable<BO.TaskInList> BOTaskList =
        from item in TaskList
        orderby item.Id
        select new BO.TaskInList//a collection of all tasks order by the id,from type TaskInList
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Descriptoin,
            Status = GetStatus(item),
            Copmlexity = (BO.EngineerLevel) item.Complexity
        };
        if (filter != null)
        { return BOTaskList.Where(filter); }//return all tasks under the condition
        else { return BOTaskList; }
    }
    /// <summary>
    ///The function needs to return a copy of the list with all the tasks of type BO.Task 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAllBOTask(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();// a collection of all the tasks
        IEnumerable<BO.Task> BOTaskList =
        from doTask in TaskList
        orderby doTask.Id
        select new BO.Task
        {
            Id = doTask.Id,
            Name = doTask.Name,
            Description = doTask.Descriptoin,
            Copmlexity = (BO.EngineerLevel)doTask.Complexity,
            EngineerTask = CalculateEngineerTask(doTask.Engineerid),
            CreatedAtDate = doTask.CreateDate,
            RequiredEffortTime = doTask.RiquiredEffortTime,
            ForecastDate = doTask.OptionalDeadline,
            StartDate = doTask.StartTaskDate,
            ScheduledDate = doTask.StartDate,
            DeadlineDate = doTask.ActualDeadline,
            Remarks = doTask.Note,
            Status = GetStatus(doTask),
            Dependencies = GetAllDependencys(doTask)
        };//a collection of all tasks order by the id,from type TaskInList
        if (filter != null)
        { return BOTaskList.Where(filter); }//return all tasks under the condition
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
        if (BlImplementation.Project.GetStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");//we can update tasks only in the Planing Stage
        if (item.Id < 0 || item.Name == "")//check if the input is logicaly correct
            throw new BlWrongInput("wrong input");
        if ((_dal.Task.Read(item.Id)!.StartDate != null) && (_dal.Task.Read(item.Id)!.StartDate != item.StartDate))//if the task  have optional start date and the user want to update it,
        {
            try { UpdateStartDate(item.Id, item.StartDate); }//calls the UpdateStartDate function to update the start date
            catch (BO.BlcanotUpdateStartdate ex)
            { throw new BO.BlcanotUpdateStartdate($"Can not update the date", ex); };
        }
        DO.Task doTask = new (item.Name, item.Description, item.Id, " ", (DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1, item.CreatedAtDate, item.RequiredEffortTime, false, item.DeadlineDate, item.ScheduledDate, item.StartDate, item.CompleteDate, item.Remarks);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={item.Id} does Not exist", ex);
        }
    }
    /// <summary>
    /// update the optional start date(we dont really use this function becuase we prefer the automatic schedulee
    /// </summary>
    /// <param name="id">id of a task</param>
    /// <param name="startDate">the date we want to put</param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlcanotUpdateStartdate"></exception>
    /// <exception cref="BO.BlTooEarlyDate"></exception>
    public bool UpdateStartDate(int id, DateTime? startDate)
    {
        if (BlImplementation.Project.GetStage() != BO.Stage.MiddleStage) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");//we can update the optional start date only in the middle stage
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);//the required task
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!.Where(item => item!.DependentTask! == id)!;//a collection of all the dependencies with this task
        IEnumerable<DateTime?>? taskDependent = from dependent in dependentList
                                                let item = _dal.Task.Read(dependent.DependentOnTask)
                                                where (item != null) && (item.StartDate == null)
                                                select item.StartDate;//collection of dates of tasks which are null that the current task dependents on
        //the collection is a counter to check if the task dependents on tasks that don't have an optional start date
        if (taskDependent.Any())//there ia a task that this task dependent on that didnt start yet
            throw new BO.BlcanotUpdateStartdate($"Can not update the date becuse task with {id} dependent on other task that didnt start yet ");
        taskDependent = from dependent in dependentList
                        let item = _dal.Task.Read(dependent.DependentOnTask)
                        where (item != null) && (item.StartDate + item.RiquiredEffortTime > startDate) && (item.StartDate >= item.CreateDate)
                        select item.StartDate;//a collection of all tasks that the current task dependents on and they finish after the requested optional start date
        if (taskDependent.Any())//there is a task that this task dependent on that finish after the new start date
            throw new BO.BlTooEarlyDate($"The date {startDate} is too early");

        DO.Task updateTask = new (doTask.Name, doTask.Descriptoin, doTask.Id, doTask.Product, doTask.Complexity, doTask.Engineerid, doTask.CreateDate, doTask.RiquiredEffortTime, false, startDate+doTask.RiquiredEffortTime, startDate, doTask.StartTaskDate, doTask.ActualDeadline, doTask.Note);
        try
        {
            _dal.Task.Update(updateTask);
            return true;
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
    public Tuple<int?, string>? CalculateEngineerTask(int? id)
    {
        //helper function 
        if (id == null) return null;
        DO.Engineer engineerName = _dal.Engineer.ReadAll().FirstOrDefault(item => item!.Id == id)!;//reads the engineer with id from the lower layer
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
    public void UpdateEngineerToTask(int idEngineer, int idTask)
    {
        if (_dal.Task.Read(idTask) == null) throw new BO.BlDoesNotExistException($"Task with ID={idTask} does Not exist");
        if (_dal.Engineer.Read(idEngineer) == null) throw new BO.BlDoesNotExistException($"Engineer with ID={idEngineer} does Not exist");
        if (BlImplementation.Project.GetStage() != BO.Stage.Doing) throw new BlNotAtTheRightStageException("can't assign engineer to the task at the current stage of the project");
        //only in stage of doing we can assign engineer to a task
        DO.Engineer? engineer = _dal.Engineer.Read(idEngineer);
        DO.Task? task = _dal.Task.Read(idTask);
        if ((task != null) && (engineer != null))
        {
            if (task.Complexity > engineer.Complexity) throw new BlCanNotAssignRequestedEngineer("the level of the engineer is too low in order to de to requested task");
           
            IEnumerable<DO.Task> taskList =
            from DO.Task item in _dal.Task.ReadAll()
            where item.Engineerid == idEngineer//the engineer is already assigned to another task
            select item;
            
            List<DO.Task> lst = (from DO.Task item in taskList//calculate which tasks from the tasks of the engineer are done
                                 where GetStatus(item) == BO.Status.Done
                                 select item).ToList();

            if (taskList.Any() && lst.Count()!=taskList.Count()) throw new BlCanNotAssignRequestedEngineer("the engineer you want to assingn is already assigned to other task");//if the engineer is already assign to tasks that didnt finish yet
            IEnumerable<DO.Task> taskDependencys =
              from DO.Dependency item in _dal.Dependency.ReadAll()
              where item.DependentTask == idTask//the task dependent on other task
              select _dal.Task.Read(item.DependentOnTask);
            if (taskDependencys != null)
            {
                taskDependencys!.Where(item => (item.StartDate + item.RiquiredEffortTime)! > _bl.Clock).ToList();//the task's that this task dependent on finish day, is before today
                if (taskDependencys==null) throw new BlCanNotAssignRequestedEngineer("The Task dependent on other tasks that wasn't done yet,can't assign the engineer to the requested task");
            }
            DO.Task taskToUpdate = new(task.Name, task.Descriptoin, task.Id, task.Product, task.Complexity, idEngineer, task.CreateDate, task.RiquiredEffortTime, false, task.OptionalDeadline, task.StartDate, task.StartTaskDate, task.ActualDeadline, task.Note);//assign the engineer to the task
            try
            {
                _dal.Task.Update(taskToUpdate);
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
    public BO.Status GetStatus(DO.Task task)
    {
        if (task.StartDate == null)
            return BO.Status.Unschedeled;
        else if (((task.StartDate != null) && (task.StartDate >= _bl.Clock) && (task.StartTaskDate == null))||(task.StartTaskDate >_bl.Clock))
            return BO.Status.Schedeled;//the optional start date is in the future and the actual start date doesn't exsit or in the future too
        else if ((task.StartDate <= _bl.Clock) && (task.StartTaskDate == null))
            return BO.Status.InJeopardy;//the optional start date is in the past and thet is no actual start date yet
        else if ((task.StartTaskDate !=null)&&(task.StartTaskDate<=_bl.Clock) && (task.StartTaskDate + task.RiquiredEffortTime >= _bl.Clock))
            return BO.Status.OnTrack;//if the actual start date in the past and the actual deadline date is in the future
        else //if (task.StartTaskDate + task.RiquiredEffortTime < DateTime.Today)
            return BO.Status.Done;
    }
    /// <summary>
    /// return a list of all dependencies that dependence on the given task
    /// </summary>
    /// <param name="task">the task we search the dependencies of it</param>
    /// <returns></returns>
    public List<TaskInList>? GetAllDependencys(DO.Task task)
    {
        IEnumerable<DO.Task> taskDependencys =
             from DO.Dependency item in _dal.Dependency.ReadAll()
             where item.DependentTask == task.Id
             select _dal.Task.Read(item.DependentOnTask);//a collection of all tasks that the current task dependents on
        List<TaskInList>? dependencys1 = (taskDependencys.Select(taskDependency => new TaskInList
        {
            Id = taskDependency.Id,
            Description = taskDependency.Descriptoin,
            Name = taskDependency.Name,
            Status = GetStatus(taskDependency)
        })).ToList();//change from task to TaskInList
        return dependencys1;
    }
    /// <summary>
    /// returns the id of the dependency
    /// </summary>
    /// <param name="idDependency">the dependent task</param>
    /// <param name="idDependentOn">the task </param>
    /// <returns></returns>
    public int FindDependent(int idDependency, int idDependentOn)
    {
        List<int> dependencysId= _dal.Dependency.ReadAll().Where(dependency=>(dependency != null) && (dependency.DependentTask == idDependency) && (dependency.DependentOnTask == idDependentOn)).Select(dependency=>dependency!.Id).ToList();
        if(dependencysId.Any())
        return dependencysId.First();
        return 0;
    }
    
    /// <summary>
    /// clear the data source
    /// </summary>
    public void Clear()
    {
        _dal.Dependency.Clear();
        _dal.Task.Clear();

    }
    /// <summary>
    /// create automatically an optional start date to all the tasks
    /// </summary>
    /// <exception cref="BlNotAtTheRightStageException"></exception>
    public void CreateAutomaticSchedule()
    {
        if (BlImplementation.Project.GetStage() != BO.Stage.MiddleStage) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        List<int> lst = new List<int>();//a list of id of tasks in specific order
        List<Tuple<int, DateTime?>>? toUpdate;//a list that we will send to update start date
        toUpdate = (from DO.Task task in _dal.Task.ReadAll()//all the tasks without start date-update with the start date of the project
                    where GetAllDependencys(task)!.Any() == false
                    select new Tuple<int, DateTime?>(task.Id, Project.GetStartProject())).ToList();
       
        foreach(var item in toUpdate) { lst.Add(item.Item1); };//put every item in toUpdate in the list of id
        while (_dal.Task.ReadAll().Count()> toUpdate.ToList().Count())//while not all the tasks in the list "toUpdate"
        {

            IEnumerable<DO.Task> tasks1 = (from DO.Task item1 in _dal.Task.ReadAll()//collection of all tasks the dependent on other task
                                           where ((GetAllDependencys(item1)!.Any()))
                                         select item1).ToList();
           foreach (DO.Task task in tasks1)
           {
                IEnumerable<DateTime?> endDate = (from BO.TaskInList dependency in GetAllDependencys(task)!//create a collection from the tasks that dependent on a certain task
                                               let doDep = _dal.Task.Read(dependency.Id)!
                                               where lst.Contains(dependency.Id)//if the dependency is in lst
                                               select (DateTime?)(toUpdate.FirstOrDefault((item=>item.Item1== dependency.Id))!.Item2 + doDep.RiquiredEffortTime)).ToList();//put in the list the end date of the dependent of task 

               if (endDate.Count() == GetAllDependencys(task)!.Count())//the collection is empty-all dependencies have starsdate
               {
                    DateTime? theDate = endDate.Max()!.Value.AddDays(1);
                    Tuple<int, DateTime?>? toPush = new Tuple<int, DateTime?>(task.Id, theDate);
                    toUpdate.Insert(toUpdate.Count(), toPush);
                    lst.Add(task.Id);
               }
                
           }


        }
        toUpdate.Select(item => UpdateStartDate(item.Item1, item.Item2)).ToList();
        IEnumerable<DateTime?> endProject= toUpdate.Select(item=>item.Item2).ToList();
        { Project.CreateEndDate(endProject.Max()); }

   }
    /// <summary>
    /// add a dependency between to tasks
    /// </summary>
    /// <param name="dependency">the task that dependenet</param>
    /// <param name="id">the task that another task dependent on</param>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public void AddDependency(int dependency,int id)
    {
        if (BlImplementation.Project.GetStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        if (dependency == id) throw new BlCantAddDependency("Task can't be dependent on itself");
        BO.Task task = Read(id)!;//dependent on
        BO.Task DepTask = Read(dependency)!;//the dependent task
        if (DepTask.Status==BO.Status.OnTrack|| DepTask.Status == BO.Status.Done) 
            throw new Exception("The Task is already Done,Cant create the dependency");
        else
        if (DepTask.Dependencies!.FirstOrDefault(item => item.Id == id) != null)
            throw new BO.BlAlreadyExistException("Such dependency is already exists");
        else
        {
            DO.Dependency newDependentDo = new(0, dependency,id);
            _dal.Dependency.Create(newDependentDo);
        }
    }
    /// <summary>
    /// delete a dependency between to tasks
    /// </summary>
    /// <param name="dependency">the task that dependenet</param>
    /// <param name="id">the task that another task dependent on</param>
    public void DeleteDependency(int dependency, int id)
    {
        if (BlImplementation.Project.GetStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");

        BO.Task task = Read(id)!;
       DO.Dependency d=_dal.Dependency.ReadAll().FirstOrDefault(item=>(item!.DependentOnTask==id)&&(item.DependentTask==dependency))!;  
        _dal.Dependency.Delete(d.Id);
    }
    /// <summary>
    /// return all the tasks that one engineer can do
    /// </summary>
    /// <param name="engineer"></param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> AllTasksToAssign(BO.Engineer engineer) 
    {
   
        List < TaskInList >? toAssign= ReadAll().Where(item => (BO.EngineerLevel)item!.Copmlexity <= engineer.Level)!.ToList()!;//all the tasks that their complexity fit the engineer's

        List<TaskInList> toAssign2=new();
        foreach (TaskInList task in toAssign)
        {
            DO.Task doTTask = _dal.Task.Read(task.Id)!;
            if ((GetAllDependencys(doTTask!)!.Any() == false)&&(doTTask.Engineerid==0)) { toAssign2.Add(task); }//if the task is not dependent on others and has not an engineer yet
            else//if the task is dependent on others
            {
                List<TaskInList>lst=GetAllDependencys(doTTask!)!;
                IEnumerable<DO.Task>deps=lst.Select(item=>_dal.Task.Read(item.Id))!;
                deps = deps.Where(item => GetStatus(item) != Status.Done).ToList();
                if((deps.Any()==false)&&(Read(task.Id)!.EngineerTask==null)) { toAssign2.Add(task); }  //if all the tasks that the task is dependent on are finished and she has no engineer yet-the engineer can assign to this task
            }
        }
        return toAssign2.ToList();   
    }
    /// <summary>
    /// updates the date that the engineer started the task
    /// </summary>
    /// <param name="actuallStartDate">th date that the enginner chose to start the task</param>
    /// <param name="id">the id of the task that the engineer want to start</param>
    public void UpdateActuallStartDate(DateTime? actuallStartDate,int id) 
    {
        try
        {
            BO.Task task = Read(id)!;
            DO.Task doTask = new(task.Name, task.Description, task.Id, "", (DO.EngineerLevel)task.Copmlexity, task.EngineerTask!.Item1, task.CreatedAtDate, task.RequiredEffortTime, false, task.ForecastDate, task.ScheduledDate, actuallStartDate, actuallStartDate + task.RequiredEffortTime, task.Remarks);
            if (DateToStart(id)!.Value > actuallStartDate!.Value) throw new BO.BlTooEarlyDate($"Task with id:{id} can't start at:{actuallStartDate}, please choose another date not earlier than:{DateToStart(id)}");//if the minimal date to start date is after the requested date
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }
    /// <summary>
    /// returns the minimal date that a task can be start according to the actual deadLine date of the tasks that it depends on
    /// </summary>
    /// <param name="id">id of the task that the engineer want to start</param>
    /// <returns></returns>
    public DateTime? DateToStart(int id)
    {
        List<TaskInList> deps=GetAllDependencys(_dal.Task.Read(id)!)!;//all the tasks that task dependents on
        if (deps.Count == 0) {
            if(_dal.Task.Read(id)!.StartDate< _bl.Clock) { return _bl.Clock; }
            return _dal.Task.Read(id)!.StartDate;
        }
        //else
        List<DateTime?> date=(from dt in deps
                             select Read(dt.Id)!.DeadlineDate).ToList();
        date.Add(_bl.Clock);
        return date.Max()!.Value.AddDays(1);
        
    }
   
}



