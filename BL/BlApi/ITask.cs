﻿namespace BlApi;

public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll();
    public void Update(BO.Task item);
    public void Delete(int id);
    public void updateStartDate(int id, DateTime startDate);
    

}
