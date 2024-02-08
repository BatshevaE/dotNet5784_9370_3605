﻿using BlApi;
using BO;
using DalApi;
using System.Data.Common;

namespace BlImplementation;

public class Project 
{
    private static  DalApi.IDal _dal = DalApi.Factory.Get;
    public static BO.Stage getStage()
    {
        //if (IBl.startWorkProject == null)
       if(_dal.StartProject == null)
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
    public static void CreateSchedele(DateTime startDate)
    {
        //if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        if (startDate >= DateTime.Now)
            //IBl.startWorkProject = startDate;
            // _dal.setStartProject(startDate);
            _dal.StartProject = startDate;
        else
            throw new BlcanotUpdateStartdate("too early date");


    }

    public static void zeroStartProject()
    {
        _dal.StartProject = null;
    }
}
