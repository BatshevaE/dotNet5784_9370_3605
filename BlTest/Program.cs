using BlApi;
using BO;
using DalApi;
using DalTest;
using DO;
using System.Xml.Linq;

namespace BlTest;

internal class Program
{
    public enum MainMenue
    {
        Exit = 0, Task, Engineer,
    }
    public enum SubMenue

    {
        Exit, Creat, Read, ReadAll, Update, Delete,
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
Update:4
Delete:5
updateStartDate:6
 ");
                if (!SubMenue.TryParse(Console.ReadLine(), out choiceTask)) //read the int choice and convert it to SubMenue types
                    throw new FormatException("wrong input");
                switch (choiceTask)
                {
                    case SubMenue.Exit:
                        return;
                    case SubMenue.Creat:
                        creatTask();
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
                    default:
                        return;
                }
            }
            while (choiceTask != 0);
        }
        catch (Exception ex) { Console.WriteLine(ex); };
    }
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
    static void creatTask()
    {
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
        while (answer != "No")
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
        static void deleteTask()
    {
        Console.WriteLine($@"Please enter the id of the task you would like to delete from the list:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        s_bl!.Task!.Delete(id);
    }
    static void readTask()
    {
        Console.WriteLine($@"Please enter the task's id that you would like to read:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        else
        {
            BO.Task? taskToRead = s_bl!.Task.Read(id)!;//stage 4

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
    static void readListTasks()
    {
        // List<DO.Task> listTasks = s_dalTask!.ReadAll();stage 1
        IEnumerable<BO.Task?> listTasks = s_bl!.Task!.ReadAll();//stage 2

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
//    static void createEngineer()
//    {
//        Console.WriteLine($@"Please enter the following details about the engineer:
//Name:");
//        string engineerName = Console.ReadLine()!;
//        Console.WriteLine($@"Id:");
//        if (!int.TryParse(Console.ReadLine(), out int engineerId))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"Cost for an hour:");
//        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"Complex of the engineer:");
//        if (!EngineerLevel.TryParse(Console.ReadLine(), out EngineerLevel engineerComplex))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"An Email address:");
//        string engineerEmail = Console.ReadLine()!;
//        Engineer engineer = new(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
//        //s_dalEngineer!.Create(engineer);stage 1
//        s_bl!.Engineer.Create(engineer);//stage 2

//        Console.WriteLine($"the id of the new engineer is:{engineerId} ");
//    }

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
            Console.WriteLine($@"The engineer's name is:{engineerToRead.Name},
The engineer's email address is:{engineerToRead.Email},
The engineer's cost for an hour is:{engineerToRead.Cost},
The engineer's complexity is:{engineerToRead.Level}.
");
        }

    }
    static void readAllEngineers()
    {
        IEnumerable<BO.Engineer?> engineers = s_bl!.Engineer.ReadAll();//stage 2            
        foreach (BO.Engineer? engineerToRead in engineers)//a loop that goes over the list of engineers
        {
            Console.WriteLine($@"The engineer's name is:{engineerToRead!.Name},
The engineer's email address is:{engineerToRead.Email},
The engineer's cost for an hour is:{engineerToRead.Cost},
The engineer's complexity is:{engineerToRead.Level}.
");
        }

    }
    static void deleteEngineer()
    {
        Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Wrong input");
        s_bl!.Engineer.Delete(id);//stage 2              

    }
}
//    static void updateEngineer()
//    {
//        Console.WriteLine($@"Please enter the following details about the engineer you would like to update:
//Name:");
//        string engineerName = Console.ReadLine()!;
//        Console.WriteLine($@"Id:");
//        if (!int.TryParse(Console.ReadLine(), out int engineerId))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"Cost for an hour:");
//        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"Complex of the engineer:");
//        if (!EngineerLevel.TryParse(Console.ReadLine(), out EngineerLevel engineerComplex))
//            throw new FormatException("Wrong input");
//        Console.WriteLine($@"An Email address:");
//        string engineerEmail = Console.ReadLine()!;
//        Engineer engineer = new(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
//        //s_dalEngineer!.Update(engineer);stage 1
//        s_dal!.Engineer.Update(engineer);//stage 2
//    }


/*
 
   
        static void updateTask()
        {
            Console.WriteLine($@"Please enter the following details about the task:
 Name:");
            string taskName = Console.ReadLine()!;
            Console.WriteLine($@"Descriptoin:");
            string taskDescriptoin = Console.ReadLine()!;
            Console.WriteLine($@"Id:");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"A task's complex:");
            if (!EngineerLevel.TryParse(Console.ReadLine(), out EngineerLevel taskComplex))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"A task's product:");
            string taskProduct = Console.ReadLine()!;
            Console.WriteLine($@"The engineer's id:");
            if (!int.TryParse(Console.ReadLine(), out int engineerId))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"The engineer's riquired effort time for the task:");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan riquiredEffortTime))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"What is the latest date for you to finish the project:");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime OptionalDeadline))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"When would you like to start the task:");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime StartDate))
                throw new FormatException("Wrong input");
            Console.WriteLine($@"Note:");
            string? note = Console.ReadLine();

            DO.Task? task = new(taskName, taskDescriptoin, id, taskProduct, taskComplex, engineerId, DateTime.Today, riquiredEffortTime, false, OptionalDeadline, StartDate, null, null, note);
            //int idTask=s_dalTask!.Create(task);stage 1
            /// int idTask=
            s_dal!.Task!.Update(task);//stage 2
        }
 */   
      
      
     
  