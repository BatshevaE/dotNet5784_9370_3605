using BlApi;
using DalApi;

namespace BlImplementation;

public class Project 
{
    private static  DalApi.IDal _dal = DalApi.Factory.Get;
    public static BO.Stage getStage()
    {
        if (IBl.startWorkProject == null)
            return BO.Stage.Planning;
        else
        {
            IEnumerable<DO.Task?> TaskList = _dal.Task.ReadAll();
            IEnumerable<DO.Task?> tasksWhithoutDate=from task in TaskList
                                                 where task.StartDate==null
                                                 select task;
            if (tasksWhithoutDate != null)
                return BO.Stage.MiddleStage;
            else return BO.Stage.Doing;

        }
    }
}
