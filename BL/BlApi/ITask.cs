namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task item);
    public void Delete(int id);
    public void UpdateStartDate(int id, DateTime? startDate);
    public void updateEngineerToTask(int idEngineer, int idTask);
    public  int FindDependent(int idDependency, int idDependentOn);
    public void clear();
    public void createAutomaticLuz();




}
