using BlApi;
using BO;
using DalApi;
using System.Data.Common;
using Dal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlImplementation;
/// <summary>
/// all methodes that are relevant to the general project(no scecific object)
/// </summary>
public class Project 
{
    /// <summary>
    /// the data source from the dal
    /// </summary>
    private static  DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// returns the stage of the project depends on the project's start date
    /// </summary>
    /// <returns></returns>
    public static BO.Stage GetStage()
    {
      if(Project.GetStartProject()==null)//no start date to the project yet
            return BO.Stage.Planning;
        else
        {
            IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();//colllection of all tasks
            IEnumerable<DO.Task?> tasksWhithoutDate=from task in TaskList
                                                 where task.StartDate==null
                                                 select task;//collection of all tasks with no optional start date
            if (tasksWhithoutDate .Any())//if all tasks doesn't have optional start date
                return BO.Stage.MiddleStage;
            else
                return BO.Stage.Doing;
        }
    }
    /// <summary>
    /// set start date to the project
    /// </summary>
    /// <param name="startDate">the date</param>
    /// <exception cref="BlcanotUpdateStartdate"></exception>
    public static void CreateSchedele(DateTime? startDate)
    {
        if ((startDate >= DateTime.Now) && (_dal.StartProject == null))//there is no start date to the project and the chosen date is not in the past
            _dal.StartProject = startDate;//set the date to be the start date of the project
        else
            throw new BlcanotUpdateStartdate("too early date");
    }
    /// <summary>
    ///set end date to the project
    /// </summary>
    /// <param name="endDate"></param>
    public static void CreateEndDate(DateTime? endDate)
    { _dal.EndProject = endDate; }
   /// <summary>
   /// initialize the project's start date to be null
   /// </summary>
    public static void ZeroStartProject()
    {
        _dal.StartProject = null;
    }
    /// <summary>
    /// returns the project's start date
    /// </summary>
    /// <returns></returns>
    public static DateTime? GetStartProject()
    {
         return _dal.StartProject;
    }
}
