﻿using BlApi;
using BO;
using DalApi;
using DalTest;
using DO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlTest;

internal class Program
{
    /// <summary>
    /// enum for the main menue
    /// </summary>
    public enum MainMenue
    {
        Exit = 0, Task, Engineer,CreateStartDate
    }
    /// <summary>
    /// enum for the sub menue
    /// </summary>
    public enum SubMenueTask

    {
        Exit, Creat, Read, ReadAll, Update, Delete,UpdateStartDate,AssignEngineerToTask
    }
    public enum SubMenueEng

    {
        Exit, Creat, Read, ReadAll, Update, Delete, EngineersAtRequestedLevel
    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
        {
            s_bl.Task.clear();
            s_bl.Engineer.clear();
            DalTest.Initialization.Do();
        }
        MainMenue choice;
        try
        {
            do
            {
                choice = MainChoice();

                switch (choice)
                {
                    case Program.MainMenue.Exit:
                        return;
                    case Program.MainMenue.Task:
                        ChoiceTask();
                        break;
                    case Program.MainMenue.Engineer:
                        ChoiceEngineer();
                        break;
                    case Program.MainMenue.CreateStartDate:
                        CreateStartDate();
                        break;
                    default:
                        return;
                }
            }
            while (choice != 0);
        }
        catch (Exception ex) { Console.WriteLine(ex); };
    }
    /// <summary>
    /// the sub menue,chose task or engineer
    /// </summary>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static MainMenue MainChoice()
    {

        Console.WriteLine(@"Choose one of the following options: 
Exit:0
Task:1
Engineer:2
Create start date of the project:3");
        if (MainMenue.TryParse(Console.ReadLine(), out MainMenue choice))
            return choice;
        else
            throw new FormatException("Wrong input");

    }
    /// <summary>
    /// if the choice is task
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void ChoiceTask()
    {
        try
        {
            SubMenueTask choiceTask;
            do
            {
                Console.WriteLine(@"Choose one of the following options for Task 
Exit:0
Creat:1
Read:2
ReadAll:3
Update generall details of task:4
Delete:5
Update Start Date of all tasks:6
Assign engineer to task:7
 ");
                if (!SubMenueTask.TryParse(Console.ReadLine(), out choiceTask)) //read the int choice and convert it to SubMenue types
                    throw new FormatException("wrong input");
                switch (choiceTask)
                {
                    case SubMenueTask.Exit:
                        return;
                    case SubMenueTask.Creat:
                        createTask();
                        break;
                    case SubMenueTask.Read:
                        readTask();
                        break;
                    case SubMenueTask.ReadAll:
                        readListTasks();
                        break;
                    case SubMenueTask.Update:
                        updateTask();
                        break;
                    case SubMenueTask.Delete:
                        deleteTask();
                        break;
                    case SubMenueTask.UpdateStartDate:
                        updateStartDate();
                        break;
                    case SubMenueTask.AssignEngineerToTask:
                        assignEngineerToTask();
                        break;
                    default:
                        return;
                }
            }
            while (choiceTask != 0);
        }
        catch (Exception ex) { Console.WriteLine(ex); };
    }
    /// <summary>
    /// if the choice is engineer
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void ChoiceEngineer()
    {
        try
        {
            SubMenueEng choiceEngineer;
            do
            {
                Console.WriteLine(@"Choose one of the following options for Engineer:
Exit:0
Creat:1
Read:2
ReadAll:3
Update:4
Delete:5
Read all engineer in certain level:6");
                if (!SubMenueEng.TryParse(Console.ReadLine(), out choiceEngineer)) //read the int choice and convert it to SubMenue types
                    throw new FormatException("wrong input");

                switch (choiceEngineer)
                {
                    case SubMenueEng.Exit:
                        return;
                    case SubMenueEng.Creat:
                        createEngineer();
                        break;
                    case SubMenueEng.Read:
                        readEngineer();
                        break;
                    case SubMenueEng.ReadAll:
                        readAllEngineers();
                        break;
                    case SubMenueEng.Update:
                        updateEngineer();
                        break;
                    case SubMenueEng.Delete:
                        deleteEngineer();
                        break;
                    case SubMenueEng.EngineersAtRequestedLevel:
                        EngineersAtRequestedLevel();
                        break;  
                    default:
                        return;
                }
            }
            while (choiceEngineer != 0);
        }
        catch (Exception ex) { Console.WriteLine(ex); };
    }
    static void CreateStartDate()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        Console.WriteLine($@"Please enter the date to start the project");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            throw new FormatException("Wrong input");
        try
        {
            BlImplementation.Project.CreateSchedele(startDate);
        }
        catch (BlcanotUpdateStartdate ex) { Console.WriteLine(ex); };

    }
    /// <summary>
    /// create a new task
    /// </summary>
    /// <exception cref="BlNotAtTheRightStageException"></exception>
    /// <exception cref="FormatException"></exception>
    static void createTask()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        Console.WriteLine($@"Please enter the following details about the task:
    Name:");
        string taskName = Console.ReadLine()!;
        Console.WriteLine($@"Descriptoin:");
        string taskDescriptoin = Console.ReadLine()!;
        Console.WriteLine($@"A task's complex:");
        if (!BO.EngineerLevel.TryParse(Console.ReadLine(), out BO.EngineerLevel taskComplex))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"The engineer's riquired effort time for the task:");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan riquiredEffortTime))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Remarks about the task:");
        string? Remarks = Console.ReadLine();
        string? answer = null;
        List<BO.TaskInList>? dependencies = new List<TaskInList>();
        while (answer != "No")//here we insert a list of tasks  that the current task depends on
        {
            Console.WriteLine($@"Does the current task depends on privious tasks?Yes/No");
            answer = Console.ReadLine();
            if (answer == "Yes")
            {
                Console.WriteLine($@"The id of the tasks that the current task depends on:");
                if (int.TryParse(Console.ReadLine(), out int dependency))
                    throw new FormatException("Wrong input");
                BO.Task? task1 = s_bl.Task.Read(dependency);
                if (task1 != null)
                {
                    TaskInList newTask = new TaskInList
                    { Id = task1.Id,
                        Description = task1.Description,
                        Name = task1.Name,
                        Status = task1.Status
                    };
                    dependencies?.Add(newTask);
                }

            }
        }
        BO.Task? task = new BO.Task
        {
            Id = 0,
            Name = taskName,
            Description = taskDescriptoin,
            CreatedAtDate = DateTime.Now,
            Status = BO.Status.Unschedeled,
            Dependencies = dependencies,
            RequiredEffortTime = riquiredEffortTime,
            StartDate = null,
            ScheduledDate = null,
            ForecastDate = null,
            DeadlineDate = null,
            CompleteDate = null,
            Remarks = Remarks,
            Copmlexity = taskComplex,
            EngineerTask = null
        };
               int idTask = s_bl!.Task!.Create(task);//stage 2
             Console.WriteLine($"The id of the new task is:{idTask}");
    }
    /// <summary>
    /// delete a task from the data source
    /// </summary>
    /// <exception cref="BlNotAtTheRightStageException"></exception>
    /// <exception cref="FormatException"></exception>
        static void deleteTask()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        Console.WriteLine($@"Please enter the id of the task you would like to delete from the list:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        s_bl!.Task!.Delete(id);
    }
    /// <summary>
    /// read a task from the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void readTask()
    {
        Console.WriteLine($@"Please enter the task's id that you would like to read:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        else
        {
            BO.Task? taskToRead = s_bl!.Task.Read(id)!;
            Console.WriteLine(taskToRead!.ToString());

        }
    }
    /// <summary>
    /// read all the tasks from the data source
    /// </summary>
    static void readListTasks()
    {
        IEnumerable<BO.Task?> listTasks = s_bl!.Task!.ReadAll();

        Console.WriteLine("The tasks are:");
        foreach (BO.Task? taskToRead in listTasks)//a loop that goes over the list of tasks
        {
            Console.WriteLine(taskToRead!.ToString());
        }
    }
   /// <summary>
   /// update a start date for all the  tasks
   /// </summary>
   /// <exception cref="BlNotAtTheRightStageException"></exception>
   /// <exception cref="FormatException"></exception>
    static void updateStartDate()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.MiddleStage) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        //foreach (var item in s_bl!.Task.ReadAll())
        //{
        Console.WriteLine($@"Please enter the id of the task");

        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Please enter the date to start the {id} task");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime planStartDate))
                throw new FormatException("Wrong input");
            s_bl!.Task!.UpdateStartDate(id, planStartDate);
        //}
    }
    /// <summary>
    /// assign Engineer To the Task
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void assignEngineerToTask()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Doing) throw new BlNotAtTheRightStageException("can't assign engineer to the task at the current stage of the project");
        Console.WriteLine($@"Please enter the id of the task and the engineer you want to assign for the task:");
        if (!int.TryParse(Console.ReadLine(), out int taskId))
            throw new FormatException("Wrong input");
        if (!int.TryParse(Console.ReadLine(), out int enginnerId))
            throw new FormatException("Wrong input");
        s_bl!.Task!.updateEngineerToTask(enginnerId,taskId);

    }
    /// <summary>
    /// add an engineer to the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void createEngineer()
    {
        
        Console.WriteLine($@"Please enter the following details about the engineer:
    Name:");
        string engineerName = Console.ReadLine()!;
        Console.WriteLine($@"Id:");
        if (!int.TryParse(Console.ReadLine(), out int engineerId))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Cost for an hour:");
        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Complex of the engineer:");
        if (!BO.EngineerLevel.TryParse(Console.ReadLine(), out BO.EngineerLevel engineerComplex))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"An Email address:");
        string engineerEmail = Console.ReadLine()!;
        BO.Engineer engineer = new BO.Engineer
        {
            Id = engineerId,
            Name = engineerName,
            Email = engineerEmail,
            Level = engineerComplex,
            Cost = engineerCost
        };
        s_bl!.Engineer.Create(engineer);
        Console.WriteLine($"the id of the new engineer is:{engineerId} ");
    }
    /// <summary>
    /// read an engineer from the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="DalDoesNotExistException"></exception> 
    static void readEngineer()
    {

        Console.WriteLine($@"Please enter the id of the engineer you would like to read:");
        if (!int.TryParse(Console.ReadLine(), out int engineerId))
            throw new FormatException("Wrong input");
        BO.Engineer? engineerToRead = new();
        engineerToRead = s_bl!.Engineer.Read(engineerId);//stage 2
        if (engineerToRead == null)
            throw new DalDoesNotExistException("The engineer with the requested id wasn't found in the list");
        else
        {
            Console.WriteLine(engineerToRead.ToString());
        }

    }
    /// <summary>
    ///  read all the engineers from the data source
    /// </summary>
    static void readAllEngineers()
    {
        IEnumerable<BO.Engineer?> engineers = s_bl!.Engineer.ReadAll();//stage 2            
        foreach (BO.Engineer? engineerToRead in engineers)//a loop that goes over the list of engineers
        {
            Console.WriteLine(engineerToRead!.ToString());
        }

    }
    /// <summary>
    /// remove an engineer from the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="DalDoesNotExistException"></exception>
    static void deleteEngineer()
    {
        if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        s_bl!.Engineer.Delete(id);        

    }
    /// <summary>
    /// update an engineer from the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void updateEngineer()
    {
        Console.WriteLine($@"Please enter the following details about the engineer:
    Name:");
        string engineerName = Console.ReadLine()!;
        Console.WriteLine($@"Id:");
        if (!int.TryParse(Console.ReadLine(), out int engineerId))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Cost for an hour:");
        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"Complex of the engineer:");
        if (!BO.EngineerLevel.TryParse(Console.ReadLine(), out BO.EngineerLevel engineerComplex))
            throw new FormatException("Wrong input");
        Console.WriteLine($@"An Email address:");
        string engineerEmail = Console.ReadLine()!;
        Tuple<int, string>? task1=null;
        if (BlImplementation.Project.getStage() == BO.Stage.Doing)//if the stage is planing the manager can assign  a task for the engineer
        {
            Console.WriteLine($@"Do you want to assign a task for the engineer {engineerName}? (Y/N)");
            string ans = Console.ReadLine()!;
            if (ans=="Y")
                task1=updateTaskToEngineer(engineerId);
        }
        BO.Engineer engineer = new BO.Engineer
        {
            Id = engineerId,
            Name = engineerName,
            Email = engineerEmail,
            Level = engineerComplex,
            Cost = engineerCost,
            Task = task1
        };
        s_bl!.Engineer.Update(engineer);
    }
    /// <summary>
    /// assign a task for the engineer
    /// </summary>
    /// <param name="id">id of the engineer</param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="BlCanNotAssignRequestedEngineer"></exception>
    static Tuple<int, string>? updateTaskToEngineer(int id)
    {
        BO.Engineer? eng = s_bl.Engineer.Read(id);

        if (BlImplementation.Project.getStage() == BO.Stage.Doing)//only in stage of doing we can assign a task for the engineer
        {
            Console.WriteLine($@"Id of a task:");
            if (!int.TryParse(Console.ReadLine(), out int taskId))
                throw new FormatException("Wrong input");
            BO.Task? task = s_bl.Task.Read(taskId);
            if ((task != null) && (eng != null) && (task.Copmlexity <= eng.Level))//only if the engineer can do the task
            {
                IEnumerable<DO.Task> taskList =
                    from DO.Task item in s_bl.Task.ReadAll()
                    where item.Engineerid == id//the engineer is already assigned to another task
                    select item;
                if (taskList != null) throw new BlCanNotAssignRequestedEngineer("the engineer you want to assingn is already assigned to other task");
               return s_bl.Engineer.CalculateTaskInEngineer(taskId)!;
            }
        }
        return null;
    }
    /// <summary>
    /// update a task from the data source
    /// </summary>
    /// <exception cref="FormatException"></exception>
    /// <exception cref="BlNotAtTheRightStageException"></exception>
    static void updateTask()
    {
        
            Console.WriteLine($@"Please enter the following details about the task you want to update:
Id:");
            if (!int.TryParse(Console.ReadLine(), out int taskId))
                throw new FormatException("Wrong input");
            BO.Task? taskToUpdate = s_bl.Task.Read(taskId);
            Console.WriteLine($@"Name:");
            string taskName = Console.ReadLine()!;
            Console.WriteLine($@"Descriptoin:");
            string taskDescriptoin = Console.ReadLine()!;
            Console.WriteLine($@"A task's complex:");
            if (!BO.EngineerLevel.TryParse(Console.ReadLine(), out BO.EngineerLevel taskComplex))
                throw new FormatException("Wrong input");
            TimeSpan riquiredEffortTime = taskToUpdate!.RequiredEffortTime;
            DateTime? startDate = taskToUpdate.StartDate;
            Console.WriteLine($@"Remarks about the task:");
            string? Remarks = Console.ReadLine();
        //    Console.WriteLine($@"Do you want to also update the dependencies of the task to other dependencies?Yes/No");
          //  string? answer = Console.ReadLine();
            List<BO.TaskInList>? dependencies = new List<TaskInList>();
          //  if (answer == "Yes")
           // {
             //   Console.WriteLine($@"please enter the id of the tasks that the current updated task depends on and when you finish please enter 0:");
             //   if (!int.TryParse(Console.ReadLine(), out int dependent))
              //      throw new FormatException("Wrong input");
               ///*/ while (dependent != 0)
              /*  {
                    BO.Task? task1 = s_bl.Task.Read(dependent);
                    if (task1 != null)
                    {
                        TaskInList newTask = new TaskInList
                        {
                            Id = task1.Id,
                            Description = task1.Description,
                            Name = task1.Name,
                            Status = task1.Status
                        };
                        dependencies?.Add(newTask);
                    }
                }
            }*/
            dependencies = taskToUpdate.Dependencies;

        if (BlImplementation.Project.getStage() == BO.Stage.MiddleStage) //throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        {
            //only in the stage planing we can change the effort time and start date of a task
            Console.WriteLine($@"The start Date of the task:");
            DateTime date;
            if (!DateTime.TryParse(Console.ReadLine(), out date))
                throw new FormatException("Wrong input");
            startDate = date;
        }
        if (BlImplementation.Project.getStage() == BO.Stage.Planning) //throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        { 
        Console.WriteLine($@"The engineer's riquired effort time for the task:");
            if (!TimeSpan.TryParse(Console.ReadLine(), out riquiredEffortTime))
                throw new FormatException("Wrong input");
        }
            BO.Task? task = new BO.Task
            {
                Id = taskId,
                Name = taskName,
                Description = taskDescriptoin,
                CreatedAtDate = DateTime.Now,
                Status = BO.Status.Unschedeled,
                Dependencies = dependencies,
                RequiredEffortTime = riquiredEffortTime,
                StartDate = startDate,
                ScheduledDate = null,
                ForecastDate = null,
                DeadlineDate = null,
                CompleteDate = null,
                Remarks = Remarks,
                Copmlexity = taskComplex,
                EngineerTask = null
            };
            s_bl!.Task!.Update(task);
        
    }
    static void EngineersAtRequestedLevel()
    {
        Console.WriteLine($@"please enter the level of engineer you would like to read:");
        if (!BO.EngineerLevel.TryParse(Console.ReadLine(), out BO.EngineerLevel engineerComplex))
            throw new FormatException("Wrong input");
        if(s_bl.Engineer.EngineersAtRequestedLevel(engineerComplex)!=null)
       foreach  (BO.Engineer engineer in s_bl.Engineer.EngineersAtRequestedLevel(engineerComplex)!)
              Console.WriteLine(engineer.ToString());
    }




}




