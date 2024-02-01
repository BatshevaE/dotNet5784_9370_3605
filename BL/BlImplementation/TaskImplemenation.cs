﻿using BlApi;
using BO;

namespace BlImplementation;

internal class TaskImplemenation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Task item)
    {
        if (item.Id < 0 || item.Name == "")
            throw new FormatException("wrong input");
        DO.Task doTask = new DO.Task
              (item.Name,item.Description,item.Id, null,(DO.EngineerLevel)item.Copmlexity, item.EngineerTask?.Item1,item.CreatedAtDate,item.RequiredEffortTime,false,item.DeadlineDate,item.ScheduledDate,item.StartDate,item.CompleteDate,item.Remarks);
        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Student with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        Tuple<int?, string>? tempEng;
        if(doTask.Engineerid==null) { tempEng = null; }
        else
        {
            DO.Engineer engineerName = _dal.Engineer.ReadAll().FirstOrDefault(item => item!.Id == doTask.Engineerid)!;
            string name = engineerName.Name;
            tempEng = new Tuple<int?, string>(doTask.Engineerid, name);
        }


        return new BO.Task()
        {
            Id = id,
            Name = doTask.Name,
            Description = doTask.Descriptoin,
            Copmlexity=(BO.EngineerLevel)doTask.Complexity,
            EngineerTask = tempEng,
            CreatedAtDate = doTask.CreateDate,
            RequiredEffortTime= doTask.RiquiredEffortTime,
            ForecastDate= doTask.OptionalDeadline,
            StartDate= doTask.StartDate,
            DeadlineDate= doTask.ActualDeadline,
            Remarks=doTask.Note
        };

    }

    public IEnumerable<TaskInList> ReadAll()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.TaskInList
                {
                    Id = doTask.Id,
                    Name = doTask.Name,
                    CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
                });

    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void updateStartDate(int id, DateTime startDate)
    {
        throw new NotImplementedException();
    }
}
