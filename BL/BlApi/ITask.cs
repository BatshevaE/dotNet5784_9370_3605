﻿namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null);
    public void Update(BO.Task item);
    public void Delete(int id);
    public bool UpdateStartDate(int id, DateTime? startDate);
    public void updateEngineerToTask(int idEngineer, int idTask);
    public  int FindDependent(int idDependency, int idDependentOn);
    public void clear();
    public void createAutomaticLuz();
    public void AddDependency(int id, int dependency);
    public void deleteDependency(int id, int dependency);
    public IEnumerable<BO.Task> ReadAll2(Func<BO.Task, bool>? filter = null);
    public bool GetAllDependentOnTasks(int id, List<int> lst);
    public IEnumerable<BO.TaskInList> AllTasksToAssign(BO.Engineer engineer);
    public void UpdateActuallStartDate(DateTime? actuallStartDate, int id);
    public DateTime? DateToStart(int id);
}
