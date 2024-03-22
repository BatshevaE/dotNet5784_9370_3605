using BlApi;
using BO;
using DalApi;
using System.Data.Common;
using Dal;

namespace BlImplementation;

public class Project 
{
    private static  DalApi.IDal _dal = DalApi.Factory.Get;
    public static BO.Stage GetStage()
    {
      if(Project.GetStartProject()==null)
            return BO.Stage.Planning;
        else
        {
            IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();
            IEnumerable<DO.Task?> tasksWhithoutDate=from task in TaskList
                                                 where task.StartDate==null
                                                 select task;
            if (tasksWhithoutDate .Any())
                return BO.Stage.MiddleStage;
            else
                return BO.Stage.Doing;
        }
    }
    public static void CreateSchedele(DateTime? startDate)
    {
        if ((startDate >= DateTime.Now )&& (_dal.StartProject == null))
            _dal.StartProject = startDate;       
        else
            throw new BlcanotUpdateStartdate("too early date");
    }
    public static void CreateEndDate(DateTime? endDate)
    { _dal.EndProject = endDate; }
    public static void ZeroStartProject()
    {
        _dal.StartProject = null;
    }
    public static DateTime? GetStartProject()
    {
         return _dal.StartProject;
    }
}
