using BlApi;
using BO;
using System.Linq;

namespace BlImplementation;

internal class TaskImplemenation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task item)
    {
        if (item.Id < 0 || item.Name == "")
            throw new BlWrongInput("wrong input");
        DO.Task doTask = new DO.Task
              (item.Name,item.Description,item.Id, " ",(DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1,item.CreatedAtDate,item.RequiredEffortTime,false,item.DeadlineDate,item.ScheduledDate,item.StartDate,item.CompleteDate,item.Remarks);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Task with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!;
        IEnumerable<DO.Dependency>? taskDependent=from dependent in dependentList
                                                  where dependent.DependentOnTask==id
                                                  select dependent;
        if (taskDependent != null)
            throw new BlCanNotDelete($"Task with ID {id} can't be deleted");
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }
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
            Copmlexity=(BO.EngineerLevel)doTask.Complexity,
            EngineerTask = calculateEngineerTask(doTask.Engineerid),
            CreatedAtDate = doTask.CreateDate,
            RequiredEffortTime= doTask.RiquiredEffortTime,
            ForecastDate= doTask.OptionalDeadline,
            StartDate= doTask.StartDate,
            DeadlineDate= doTask.ActualDeadline,
            Remarks=doTask.Note
        };

    }

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
            Remarks = item.Note
        };
        if (filter != null)
        { return BOTaskList.Where(filter); }

        else { return BOTaskList; }
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void updateStartDate(int id, DateTime startDate)
    {
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Id == id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        IEnumerable<DO.Dependency>? dependentList = _dal.Dependency.ReadAll()!.Where(item => item!.DependentTask! == id)!;
        IEnumerable<DateTime?>? taskDependent = from dependent in dependentList
                                                let item = _dal.Task.Read(dependent.DependentOnTask)
                                                where (item != null) && (item.StartDate == null)
                                                select item.StartDate;
        if (taskDependent != null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        taskDependent = from dependent in dependentList
                        let item = _dal.Task.Read(dependent.DependentOnTask)
                        where (item != null) && (item.StartDate < startDate)
                        select item.StartDate;
        if (taskDependent != null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        DO.Task updateTask = new DO.Task
         (doTask.Name, doTask.Descriptoin, doTask.Id, doTask.Product, doTask.Complexity, doTask.Engineerid, doTask.CreateDate, doTask.RiquiredEffortTime, false, doTask.OptionalDeadline, startDate, doTask.StartTaskDate, doTask.ActualDeadline, doTask.Note);
        try
        {
            _dal.Task.Update(updateTask);

        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", ex);
        }
    }

        public Tuple<int?,string>? calculateEngineerTask(int? id)
    {
        if (id == null) return null;
        DO.Engineer engineerName = _dal.Engineer.ReadAll().FirstOrDefault(item => item!.Id == id)!;
        string name = engineerName.Name;
        return new Tuple<int?, string>(id, name);
    }
}
