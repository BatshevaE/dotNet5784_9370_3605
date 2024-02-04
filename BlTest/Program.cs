using BlApi;
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
        Exit = 0, Task, Engineer,
    }
    /// <summary>
    /// enum for the sub menue
    /// </summary>
    public enum SubMenue

    {
        Exit, Creat, Read, ReadAll, Update, Delete,UpdateStartDate,AssignEngineerToTask
    }


    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

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
Engineer:2");
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
            SubMenue choiceTask;
            do
            {
                Console.WriteLine(@"Choose one of the following options for Task 
Exit:0
Creat:1
Read:2
ReadAll:3
Update generall details of task:4
Delete:5
Update Start Date of task:6
Assign engineer to task:7
 ");
                if (!SubMenue.TryParse(Console.ReadLine(), out choiceTask)) //read the int choice and convert it to SubMenue types
                    throw new FormatException("wrong input");
                switch (choiceTask)
                {
                    case SubMenue.Exit:
                        return;
                    case SubMenue.Creat:
                        createTask();
                        break;
                    case SubMenue.Read:
                        readTask();
                        break;
                    case SubMenue.ReadAll:
                        readListTasks();
                        break;
                    case SubMenue.Update:
                        updateTask();
                        break;
                    case SubMenue.Delete:
                        deleteTask();
                        break;
                    case SubMenue.UpdateStartDate:
                        updateStartDate();
                        break;
                    case SubMenue.AssignEngineerToTask:
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
            SubMenue choiceEngineer;
            do
            {
                Console.WriteLine(@"Choose one of the following options for Engineer:
Exit:0
Creat:1
Read:2
ReadAll:3
Update:4
Delete:5");
                if (!SubMenue.TryParse(Console.ReadLine(), out choiceEngineer)) //read the int choice and convert it to SubMenue types
                    throw new FormatException("wrong input");

                switch (choiceEngineer)
                {
                    case SubMenue.Exit:
                        return;
                    case SubMenue.Creat:
                        createEngineer();
                        break;
                    case SubMenue.Read:
                        readEngineer();
                        break;
                    case SubMenue.ReadAll:
                        readAllEngineers();
                        break;
                    case SubMenue.Update:
                        updateEngineer();
                        break;
                    case SubMenue.Delete:
                        deleteEngineer();
                        break;
                    default:
                        return;
                }
            }
            while (choiceEngineer != 0);
        }
        catch (Exception ex) { Console.WriteLine(ex); };
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
        string? Remarks = Console.ReadLine();
        string? answer = null;
        List<BO.TaskInList>? dependencies = null;
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

            Console.WriteLine($@"The task's id is:{taskToRead.Id},
The task's name is:{taskToRead.Name},
The task's descriptoin  is:{taskToRead.Description},
The task's create date is:{taskToRead.CreatedAtDate}.
The task's Status is:{taskToRead.Status}.
The task's Dependencies is:{taskToRead.Dependencies}.
The task's riquired effort time is:{taskToRead.RequiredEffortTime}.   
The task's StartDate is:{taskToRead.StartDate}.   
The task's ScheduledDate is:{taskToRead.ScheduledDate}.   
The task's ForecastDate is:{taskToRead.ForecastDate}.   
The task's DeadlineDate is:{taskToRead.DeadlineDate}.   
The task's CompleteDate is:{taskToRead.CompleteDate}.   
The task's Remarks is:{taskToRead.Remarks}.   
The task's complexity is:{taskToRead.Copmlexity},");
            if (taskToRead.EngineerTask == null)
                Console.WriteLine($@"the task does not  have an engineer yet");
            else
                Console.WriteLine($@"The task's engineer is:{taskToRead.EngineerTask}");
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
            Console.WriteLine($@"The task's id is:{taskToRead!.Id},
The task's name is:{taskToRead.Name},
The task's descriptoin  is:{taskToRead.Description},
The task's create date is:{taskToRead.CreatedAtDate}.
The task's Status is:{taskToRead.Status}.
The task's Dependencies is:{taskToRead.Dependencies}.
The task's riquired effort time is:{taskToRead.RequiredEffortTime}.   
The task's StartDate is:{taskToRead.StartDate}.   
The task's ScheduledDate is:{taskToRead.ScheduledDate}.   
The task's ForecastDate is:{taskToRead.ForecastDate}.   
The task's DeadlineDate is:{taskToRead.DeadlineDate}.   
The task's CompleteDate is:{taskToRead.CompleteDate}.   
The task's Remarks is:{taskToRead.Remarks}.   
The task's complexity is:{taskToRead.Copmlexity},");
            if (taskToRead.EngineerTask == null)
                Console.WriteLine($@"the task does not  have an engineer yet");
            else
                Console.WriteLine($@"The task's engineer is:{taskToRead.EngineerTask}");
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
        foreach (var item in s_bl!.Task.ReadAll())
        {
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime planStartDate))
                throw new FormatException("Wrong input");
            s_bl!.Task!.UpdateStartDate(item.Id, planStartDate);
        }
    }
    /// <summary>
    /// assign Engineer To the Task
    /// </summary>
    /// <exception cref="FormatException"></exception>
    static void assignEngineerToTask()
    {
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
            Console.WriteLine($@"The engineer's id is:{engineerToRead?.Id}
The engineer's name is:{engineerToRead!.Name},
The engineer's email address is:{engineerToRead.Email},
The engineer's cost for an hour is:{engineerToRead.Cost},
The engineer's complexity is:{engineerToRead.Level}.
");
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
            Console.WriteLine($@"The engineer's id is:{engineerToRead?.Id}
The engineer's name is:{engineerToRead!.Name},
The engineer's email address is:{engineerToRead.Email},
The engineer's cost for an hour is:{engineerToRead.Cost},
The engineer's complexity is:{engineerToRead.Level}.
");
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
                if (taskList != null) throw new BlCanNotAssignRequestedEngineer("the engineer you want to assingn is alreade assigned to other task");
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
        Console.WriteLine($@"Please enter the following details about the task:
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
        if (BlImplementation.Project.getStage() != BO.Stage.Planning) throw new BlNotAtTheRightStageException("you are not at the right stage of the project for the requested action");
        {//only in the stage planing we can change the effort time and start date of a task
            Console.WriteLine($@"The engineer's riquired effort time for the task:");
            if (!TimeSpan.TryParse(Console.ReadLine(), out riquiredEffortTime))
                throw new FormatException("Wrong input");
            //Console.WriteLine($@"The start Date of the task:");
            //if (!DateTime.TryParse(Console.ReadLine(), out startDate))
            //    throw new FormatException("Wrong input");
        }
        string? Remarks = Console.ReadLine();
        BO.Task? task = new BO.Task
        {
            Id = taskId,
            Name = taskName,
            Description = taskDescriptoin,
            CreatedAtDate = DateTime.Now,
            Status = BO.Status.Unschedeled,
            Dependencies = taskToUpdate.Dependencies,
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





}




