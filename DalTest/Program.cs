using Dal;
using DalApi;
using DO;

namespace DalTest
{    
    /// <summary>
    /// the main class
    /// </summary>
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

        /// <summary>
        /// enum for the main menue
        /// </summary>
        public enum MainMenue
        {
            Exit = 0, Task, Engineer, Dependency
        }
       
        public enum SubMenue
            
        {
            Exit, Creat, Read, ReadAll, Update, Delete
        }
        /// <summary>
        /// The main main that calls to choice
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {               
                Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);
                MainMenue choice;                
                do
                {
                    choice = MainChoice();
              
                    switch (choice)
                    {
                        case Program.MainMenue.Exit:
                            break;
                        case Program.MainMenue.Task:
                            ChoiceTask();
                            break;
                        case Program.MainMenue.Engineer:
                            ChoiceEngineer();
                            break;
                        case Program.MainMenue.Dependency:
                            ChoiceDependency();
                            break;
                        default:
                            break;
                    }
                }
                while (choice != 0);
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }
        /// <summary>
        /// A function that gets from the user his chice(task/engineer/dependency/exit)
        /// </summary>
        /// <returns>the choice of the user</returns>
        /// <exception cref="Exception">Wrong input</exception>
        static MainMenue MainChoice()
            {
                Console.WriteLine(@"Choose one of the following options: 
Exit:0
Task:1
Engineer:2
Dependency:3");
                if (MainMenue.TryParse(Console.ReadLine(), out MainMenue choice))
                    return choice;
                else
                    throw new Exception("Wrong input");
            }

        /// <summary>
        /// A method for the chosen option-task
        /// </summary>
        static void ChoiceTask()
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
 ");
                if (!SubMenue.TryParse(Console.ReadLine(), out  choiceTask)) //read the int choice and convert it to SubMenue types
                    throw new Exception("wrong input");           
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
                        break;
                }
            }
            while (choiceTask!=0);
            }
        /// <summary>
        /// A method for the chosen option-engineer
        /// </summary>
        static void ChoiceEngineer()
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
                if (!SubMenue.TryParse(Console.ReadLine(), out  choiceEngineer)) //read the int choice and convert it to SubMenue types
                    throw new Exception("wrong input");
           
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
                        break;
                }
            }
            while (choiceEngineer != 0);            
            }
        /// <summary>
        /// A method for the chosen option-dependency
        /// </summary>
        static void ChoiceDependency()
            {
            SubMenue choiceDependency;
            do
            {
                Console.WriteLine(@"Choose one of the following options for Dependency: 
Exit:0
Creat:1
Read:2
ReadAll:3
Update:4
Delete:5");
                if (!SubMenue.TryParse(Console.ReadLine(), out choiceDependency)) //read the int choice and convert it to SubMenue types
                    throw new Exception("wrong input");
                switch (choiceDependency)
                {
                    case SubMenue.Exit:
                        return;
                    case SubMenue.Creat:
                        createDependency();
                        break;
                    case SubMenue.Read:
                        readDependency();
                        break;
                    case SubMenue.ReadAll:
                        readAllDependencies();
                        break;
                    case SubMenue.Update:
                        updateDependency();
                        break;
                    case SubMenue.Delete:
                        deleteDependency();
                        break;
                    default:
                        break;
                }
            }
            while (choiceDependency != 0);
        }

        /// <summary>
        /// get all the details of a task, craete a new task and add it to the list of tasks
        /// </summary>
        static void creatTask()
            {
                Console.WriteLine($@"Please enter the following details about the task:
 Name:");
                string taskName = Console.ReadLine()!;
                Console.WriteLine($@"Descriptoin:");
                string taskDescriptoin = Console.ReadLine()!;
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
                DO.Task? task = new(taskName, taskDescriptoin, 0, taskProduct, taskComplex, engineerId, DateTime.Today, riquiredEffortTime, false, OptionalDeadline, StartDate, null, null, note);
                int idTask=s_dalTask!.Create(task);
                Console.WriteLine($"The id of the new task is:{idTask} ");                
            }
        /// <summary>
        /// get all the details of a task, check if the new task in the list and if is - change the details
        /// </summary>
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
                s_dalTask!.Update(task);               
            }
        /// <summary>
        /// get id of task and delete the task with this id from the list
        /// </summary>
        static void deleteTask()
            {
                Console.WriteLine($@"Please enter the id of the task you would like to delete from the list:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("Wrong input");
                s_dalTask!.Delete(id);                
            }
        /// <summary>
        /// gets a id of a task and print the task's details
        /// </summary>
        static void readTask()
            {
                Console.WriteLine($@"Please enter the task's id that you would like to read:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("Wrong input");
                else
                {
                    DO.Task? taskToRead = s_dalTask!.Read(id)!;
                    Console.WriteLine($@"The task's name is:{taskToRead.Name},
The task's descriptoin  is:{taskToRead.Descriptoin},
The task's id  is:{taskToRead.Id},
The task's complexity is:{taskToRead.Complexity},
The task's product is:{taskToRead.Product}");
                    if (taskToRead.Engineerid == null)
                        Console.WriteLine($@"the task does not  have an engineer yet");
                    else
                        Console.WriteLine($@"The task's engineer id is:{taskToRead.Engineerid}");
                    Console.WriteLine($@"The task's riquired effort time  is:{taskToRead.RiquiredEffortTime}.                                              
The task's optional dead line  is:{taskToRead.OptionalDeadline}.
The task's create date is:{taskToRead.CreateDate}.
The task's start date is:{taskToRead.StartDate}.
The task's start task date id is:{taskToRead.StartTaskDate}.
The task's actual dead line is:{taskToRead.ActualDeadline}.
");
                    if (taskToRead.Note != null)
                        Console.WriteLine($@"The task's Notes are:{taskToRead.Note}");
                }
            }
        /// <summary>
        /// prints all the tasks in the list
        /// </summary>
        static void readListTasks()
            {
                List<DO.Task> listTasks = s_dalTask!.ReadAll();
                if (listTasks.Count == 0)
                    throw new Exception("The list of tasks is empty");
                else
                {
                    Console.WriteLine("The tasks are:");
                    foreach (DO.Task task in listTasks)//a loop that goes over the list of tasks
                    {
                        Console.WriteLine($@"The task's name is:{task.Name},
The task's descriptoin  is:{task.Descriptoin},
The task's id  is:{task.Id},
The task's complexity is:{task.Complexity},
The task's product is:{task.Product}");
                        if (task.Engineerid == null)
                            Console.WriteLine($@"the task does not  have an engineer yet");
                        else
                            Console.WriteLine($@"The task's engineer id is:{task.Engineerid}");
                        Console.WriteLine($@"The task's riquired effort time  is:{task.RiquiredEffortTime}.                                               
The task's optional dead line  is:{task.OptionalDeadline}.
The task's create date is:{task.CreateDate}.
The task's start date is:{task.StartDate}.
The task's start task date id is:{task.StartTaskDate}.
The task's actual dead line is:{task.ActualDeadline}.
 ");
                        if (task.Note != null)
                            Console.WriteLine($@"The task's Notes are:{task.Note}");
                    }
                }
            }
        /// <summary>
        /// gets all the details of an engineer, craetes a new task and adds it to the list of engineer
        /// </summary>
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
                if (!EngineerLevel.TryParse(Console.ReadLine(), out EngineerLevel engineerComplex))
                throw new FormatException("Wrong input");
                Console.WriteLine($@"An Email address:");
                string engineerEmail = Console.ReadLine()!;
                Engineer engineer = new(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                s_dalEngineer!.Create(engineer);
                Console.WriteLine($"the id of the new engineer is:{engineerId} ");              
            }
        /// <summary>
        /// gets a id of an engineer and print the task's details
        /// </summary>
        static void readEngineer()
            {
                
                Console.WriteLine($@"Please enter the id of the engineer you would like to read:");
                if (!int.TryParse(Console.ReadLine(), out int engineerId))
                    throw new FormatException("Wrong input");
                Engineer? engineerToRead = new();
                engineerToRead = s_dalEngineer!.Read(engineerId);
                if (engineerToRead == null)
                    throw new Exception("The engineer with the requested id wasn't found in the list");
                else
                {
                    Console.WriteLine($@"The engineer's name is:{engineerToRead.Name},
The engineer's email address is:{engineerToRead.EmailAsress},
The engineer's cost for an hour is:{engineerToRead.CostForHour},
The engineer's complexity is:{engineerToRead.Complexity}.
");
                }
               
            }
        /// <summary>
        ///print all the engineers in the list
        /// </summary>
        static void readAllEngineers()
            {
                
                List<Engineer> engineers = s_dalEngineer!.ReadAll();
                if (engineers.Count == 0)
                    throw new Exception("The list of engineers is empty");
                else
                {
                    foreach (Engineer engineer in engineers)//a loop that goes over the list of engineers
                    {
                        Console.WriteLine($@"The engineer's id is:{engineer.Id}
The engineer's name is:{engineer.Name},
The engineer's email address is:{engineer.EmailAsress},
The engineer's cost for an hour is:{engineer.CostForHour},
The engineer's complexity is:{engineer.Complexity}.
");
                    }
                }

                // }
                //catch (Exception ex) { Console.WriteLine(ex); };
            }
        /// <summary>
        ///get all the details of an engineer, check if the new engineer in the list and if yes-change the details
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void updateEngineer()
            {            
                Console.WriteLine($@"Please enter the following details about the engineer you would like to update:
Name:");
                string engineerName = Console.ReadLine()!;
                Console.WriteLine($@"Id:");
                if (!int.TryParse(Console.ReadLine(), out int engineerId))
                    throw new FormatException("Wrong input");
                Console.WriteLine($@"Cost for an hour:");
                if (!double.TryParse(Console.ReadLine(), out double engineerCost))
                    throw new FormatException("Wrong input");
                Console.WriteLine($@"Complex of the engineer:");
                if (!EngineerLevel.TryParse(Console.ReadLine(), out EngineerLevel engineerComplex))
                throw new FormatException("Wrong input");
                Console.WriteLine($@"An Email address:");
                string engineerEmail = Console.ReadLine()!;
                Engineer engineer = new(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                s_dalEngineer!.Update(engineer);               
            }
        /// <summary>
        ///get id of engineer and delete the engineer with this id from the list 
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void deleteEngineer()
            {
                Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new FormatException("Wrong input");
                s_dalEngineer!.Delete(id);              
            }
        /// <summary>
        ///get all the details of a dependency, craet a new task and add it to the list of taskdependencys 
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void createDependency()
            {
                Console.WriteLine(@$"Please enter the following details about the task you would like to create:
The dependent task");
                if (!int.TryParse(Console.ReadLine(), out int dependTaskNum))
                    throw new FormatException("Wrong input");
                Console.WriteLine("The number of the task that needs to be done before:");
                if (!int.TryParse(Console.ReadLine(), out int firstTaskaskNum))
                    throw new FormatException("Wrong input");
                Dependency dependence = new(0, dependTaskNum, firstTaskaskNum);
                Console.WriteLine("The id of the dependency is: " + s_dalDependency!.Create(dependence));              
            }
        /// <summary>
        ///gets a id of a dependency and print the task's details
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void readDependency()
            {
                Console.WriteLine($@"Please enter the id of the dependency you would like to read");
                if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                    throw new FormatException("Wrong input");
                Dependency? dependencyToRead = new();
                dependencyToRead = s_dalDependency!.Read(dependencyId);                            
                Console.WriteLine($@"The dependent task's number is:{dependencyToRead!.DependentTask},
The task depends on task number:{dependencyToRead.DependentOnTask}.");                          
            }
        /// <summary>
        ///print all the dependencys in the list
        /// </summary>
        /// <exception cref="Exception">The list of dependencies is empty</exception>
        static void readAllDependencies()
            {
                List<Dependency> dependencies = s_dalDependency!.ReadAll();
                if (dependencies.Count == 0)
                 Console.WriteLine("The list of dependencies is empty");
                else
                {
                    foreach (Dependency dependency in dependencies)//a loop that goes over the list of dependencies
                    {
                        Console.WriteLine($@"The dependent task's number is:{dependency.DependentTask},
The task depends on task number:{dependency.DependentOnTask}.
");
                    }
                }
            }
        /// <summary>
        ///get all the details of a dependency, check if the new dependency in the list and if yes-change the details
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void updateDependency()
            {
                Console.WriteLine(@$"Please enter the following details about the task you would like to update:
The dependent's id:");
                if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                    throw new FormatException("Wrong input");
                Console.WriteLine(@$"The dependent task's number:");
                if (!int.TryParse(Console.ReadLine(), out int dependTaskNum))
                    throw new FormatException("Wrong input");
                Console.WriteLine("The number of the task that needs to be done before:");
                if (!int.TryParse(Console.ReadLine(), out int firstTaskaskNum))
                    throw new FormatException("Wrong input");
                Dependency dependence = new(dependencyId, dependTaskNum, firstTaskaskNum);
                s_dalDependency!.Update(dependence);
        }
        /// <summary>
        /// deletes the requested dependency
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void deleteDependency()
            {
                Console.WriteLine($@"Please enter the id of the dependency you would like to delete:");
                if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                    throw new FormatException("Wrong input");
                s_dalDependency!.Delete(dependencyId);
            }
        }
}
  



   





