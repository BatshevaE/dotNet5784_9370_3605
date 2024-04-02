namespace BlApi;

public interface ITask
{
    /// <summary>
    /// creates a new task 
    /// </summary>
    /// <param name="item">the details of the new task</param>
    /// <returns></returns>
    public int Create(BO.Task item);
    /// <summary>
    /// returns task with requested id
    /// </summary>
    /// <param name="id">the id of the task to read</param>
    /// <returns></returns>
    public BO.Task? Read(int id);
    /// <summary>
    /// returns all task as TaskInList
    /// </summary>
    /// <param name="filter">the user can read all task under a certion condition</param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null);
    /// <summary>
    /// update a task
    /// </summary>
    /// <param name="item">the updated task</param>
    public void Update(BO.Task item);
    /// <summary>
    /// deletes a task
    /// </summary>
    /// <param name="id">the id of the task to dekete</param>
    public void Delete(int id);
    /// <summary>
    /// update the optional start date of the task
    /// </summary>
    /// <param name="id">id of the task to update</param>
    /// <param name="startDate">the optional date</param>
    /// <returns></returns>
    public bool UpdateStartDate(int id, DateTime? startDate);
    /// <summary>
    /// assigns an engineer to task
    /// </summary>
    /// <param name="idEngineer">the enginner to assign</param>
    /// <param name="idTask">the task to assign the engineer to</param>
    public void UpdateEngineerToTask(int idEngineer, int idTask);
    /// <summary>
    /// returns the id of the dependency
    /// </summary>
    /// <param name="idDependency">the dependent task</param>
    /// <param name="idDependentOn">the task </param>
    /// <returns>returns the id of the dependency itselves</returns>
    public int FindDependent(int idDependency, int idDependentOn);
    /// <summary>
    /// clears all data about tasks and dependencies
    /// </summary>
    public void Clear();
    /// <summary>
    /// creates optional start date to all tasks
    /// </summary>
    public void CreateAutomaticSchedule();
    /// <summary>
    /// add a dependency between to tasks
    /// </summary>
    /// <param name="dependency">the task that dependenet</param>
    /// <param name="id">the task that another task dependent on</param>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public void AddDependency(int id, int dependency);
    /// <summary>
    /// delete a dependency between to tasks
    /// </summary>
    /// <param name="dependency">the task that dependenet</param>
    /// <param name="id">the task that another task dependent on</param>
    public void DeleteDependency(int id, int dependency);
    /// <summary>
    /// returns all tasks as a BO.Task
    /// </summary>
    /// <param name="filter">the user can read all task under a certion condition</param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAllBOTask(Func<BO.Task, bool>? filter = null);
   
    /// <summary>
    /// returns all tasks that a certion enginner can do
    /// </summary>
    /// <param name="engineer">the engineer ti assign</param>
    /// <returns></returns>
    public IEnumerable<BO.TaskInList> AllTasksToAssign(BO.Engineer engineer);
    /// <summary>
    /// updates the date that the engineer started the task
    /// </summary>
    /// <param name="actuallStartDate">th date that the enginner chose to start the task</param>
    /// <param name="id">the id of the task that the engineer want to start</param>
    public void UpdateActuallStartDate(DateTime? actuallStartDate, int id);
    /// <summary>
    /// returns the minimal date that a task can be start according to the actual deadLine date of the tasks that it depends on
    /// </summary>
    /// <param name="id">id of the task that the engineer want to start</param>
    /// <returns></returns>
    public DateTime? DateToStart(int id);
    
}
