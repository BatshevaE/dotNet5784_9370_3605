﻿using Dal;
using DalApi;
using DO;
namespace DalTest
{
    /// <summary> 
    /// the main class
    /// </summary>
    internal class Program
    {
        //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        //static readonly IDal s_dal = new DalList(); //stage 2
       // static readonly IDal s_dal = new DalXml();//stage 3
       static readonly IDal s_dal = Factory.Get; //stage 4


        /// <summary>
        /// enum for the main menue
        /// </summary>
        public enum MainMenue
        {
            Exit = 0, Task, Engineer, Dependency, Initialization
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
                //Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);stage 1
               
                Main1();

            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }
        public static void Main1()
        {
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
                        case Program.MainMenue.Dependency:
                            ChoiceDependency();
                            break;
                        case Program.MainMenue.Initialization:
                            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                            if (ans == "Y") //stage 3
                            {
                                s_dal.Engineer.Clear();
                                s_dal.Task.Clear();
                                s_dal.Dependency.Clear();
                                s_dal.User.Clear();
                                // Initialization.Do(s_dal); //stage 2
                                Initialization.Do(); //stage 4

                            }
                            break;
                        default:
                            return;
                    }
                }
                while (choice != 0);
            }
            catch (Exception ex) { Console.WriteLine(ex); Main1(); }
        }
        /// <summary>
        /// A function that gets from the user his chice(task/engineer/dependency/exit)
        /// </summary>
        /// <returns>the choice of the user</returns>
        /// <exception cref="FormatException">Wrong input</exception>

        public static MainMenue MainChoice()
        {

            Console.WriteLine(@"Choose one of the following options: 
Exit:0
Task:1
Engineer:2
Dependency:3
Initialization:4");
            if (MainMenue.TryParse(Console.ReadLine(), out MainMenue choice))
                return choice;
            else
                throw new FormatException("Wrong input");

        }

        /// <summary>
        /// A method for the chosen option-task
        /// </summary>
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
 ");
                    if (!SubMenue.TryParse(Console.ReadLine(), out choiceTask)) //read the int choice and convert it to SubMenue types
                        throw new FormatException("wrong input");
                    switch (choiceTask)
                    {
                        case SubMenue.Exit:
                            return;
                        case SubMenue.Creat:
                            CreatTask();
                            break;
                        case SubMenue.Read:
                            ReadTask();
                            break;
                        case SubMenue.ReadAll:
                            ReadListTasks();
                            break;
                        case SubMenue.Update:
                            UpdateTask();
                            break;
                        case SubMenue.Delete:
                            DeleteTask();
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
        /// A method for the chosen option-engineer
        /// </summary>
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
                            CreateEngineer();
                            break;
                        case SubMenue.Read:
                            ReadEngineer();
                            break;
                        case SubMenue.ReadAll:
                            ReadAllEngineers();
                            break;
                        case SubMenue.Update:
                            UpdateEngineer();
                            break;
                        case SubMenue.Delete:
                            DeleteEngineer();
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
        /// A method for the chosen option-dependency
        /// </summary>
        static void ChoiceDependency()
        {
            try
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
                        throw new FormatException("wrong input");
                    switch (choiceDependency)
                    {
                        case SubMenue.Exit:
                            return;
                        case SubMenue.Creat:
                            CreateDependency();
                            break;
                        case SubMenue.Read:
                            ReadDependency();
                            break;
                        case SubMenue.ReadAll:
                            ReadAllDependencies();
                            break;
                        case SubMenue.Update:
                            UpdateDependency();
                            break;
                        case SubMenue.Delete:
                            DeleteDependency();
                            break;
                        default:
                            return;
                    }
                }
                while (choiceDependency != 0);
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }

        /// <summary>
        /// get all the details of a task, craete a new task and add it to the list of tasks
        /// </summary>
        static void CreatTask()
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
            //int idTask=s_dalTask!.Create(task);stage 1
            int idTask = s_dal!.Task!.Create(task);//stage 2
            Console.WriteLine($"The id of the new task is:{idTask}");
        }
        /// <summary>
        /// get all the details of a task, check if the new task in the list and if is - change the details
        /// </summary>
        static void UpdateTask()
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
        /// <summary>
        /// get id of task and delete the task with this id from the list
        /// </summary>
        static void DeleteTask()
        {
            Console.WriteLine($@"Please enter the id of the task you would like to delete from the list:");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new FormatException("Wrong input");
            //s_dalTask!.Delete(id);stage 1
            s_dal!.Task!.Delete(id);//stage 2
        }
        /// <summary>
        /// gets a id of a task and print the task's details
        /// </summary>
        static void ReadTask()
        {
            Console.WriteLine($@"Please enter the task's id that you would like to read:");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new FormatException("Wrong input");
            else
            {
                //DO.Task? taskToRead = s_dalTask!.Read(id)!;stage 1
                DO.Task? taskToRead = s_dal!.Task.Read(id)!;//stage 2

                Console.WriteLine($@"The task's name is:{taskToRead.Name},
The task's descriptoin  is:{taskToRead.Descriptoin},
The task's id is:{taskToRead.Id},
The task's complexity is:{taskToRead.Complexity},
The task's product is:{taskToRead.Product}");
                if (taskToRead.Engineerid == null)
                    Console.WriteLine($@"the task does not  have an engineer yet");
                else
                    Console.WriteLine($@"The task's engineer id is:{taskToRead.Engineerid}");
                Console.WriteLine($@"The task's riquired effort time is:{taskToRead.RiquiredEffortTime}.                                              
The task's optional dead line is:{taskToRead.OptionalDeadline}.
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
        static void ReadListTasks()
        {
            // List<DO.Task> listTasks = s_dalTask!.ReadAll();stage 1
            IEnumerable<DO.Task?> listTasks = s_dal!.Task!.ReadAll();//stage 2

            Console.WriteLine("The tasks are:");
            foreach (DO.Task? task in listTasks)//a loop that goes over the list of tasks
            {
                Console.WriteLine($@"The task's name is:{task?.Name},
The task's descriptoin is:{task?.Descriptoin},
The task's id is:{task?.Id},
The task's complexity is:{task?.Complexity},
The task's product is:{task?.Product}");
                if (task?.Engineerid == null)
                    Console.WriteLine($@"the task does not  have an engineer yet");
                else
                    Console.WriteLine($@"The task's engineer id is:{task.Engineerid}");
                Console.WriteLine($@"The task's riquired effort time is:{task?.RiquiredEffortTime}.                                               
The task's optional dead line is:{task?.OptionalDeadline}.
The task's create date is:{task?.CreateDate}.
The task's start date is:{task?.StartDate}.
The task's start task date id is:{task?.StartTaskDate}.
The task's actual dead line is:{task?.ActualDeadline}.
 ");
                if (task?.Note != null)
                    Console.WriteLine($@"The task's Notes are:{task?.Note}");
            }
        }
        /// <summary>
        /// gets all the details of an engineer, craetes a new task and adds it to the list of engineer
        /// </summary>
        static void CreateEngineer()
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
            //s_dalEngineer!.Create(engineer);stage 1
            s_dal!.Engineer.Create(engineer);//stage 2

            Console.WriteLine($"the id of the new engineer is:{engineerId} ");
        }
        /// <summary>
        /// gets a id of an engineer and print the task's details
        /// </summary>
        static void ReadEngineer()
        {

            Console.WriteLine($@"Please enter the id of the engineer you would like to read:");
            if (!int.TryParse(Console.ReadLine(), out int engineerId))
                throw new FormatException("Wrong input");
            Engineer? engineerToRead = new();
            //engineerToRead = s_dalEngineer!.Read(engineerId);stage 1
            engineerToRead = s_dal!.Engineer.Read(engineerId);//stage 2
            if (engineerToRead == null)
                throw new DalDoesNotExistException("The engineer with the requested id wasn't found in the list");
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
        static void ReadAllEngineers()
        {

            //List<Engineer> engineers = s_dalEngineer!.ReadAll();stage 1
            IEnumerable<Engineer?> engineers = s_dal!.Engineer.ReadAll();//stage 2            
            foreach (Engineer? engineer in engineers)//a loop that goes over the list of engineers
            {
                Console.WriteLine($@"The engineer's id is:{engineer?.Id}
The engineer's name is:{engineer?.Name},
The engineer's email address is:{engineer?.EmailAsress},
The engineer's cost for an hour is:{engineer?.CostForHour},
The engineer's complexity is:{engineer?.Complexity}.
");
            }
        }
        /// <summary>
        ///get all the details of an engineer, check if the new engineer in the list and if yes-change the details
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void UpdateEngineer()
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
            //s_dalEngineer!.Update(engineer);stage 1
            s_dal!.Engineer.Update(engineer);//stage 2
        }
        /// <summary>
        ///get id of engineer and delete the engineer with this id from the list 
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void DeleteEngineer()
        {
            Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new FormatException("Wrong input");
            //s_dalEngineer!.Delete(id);stage 1
            s_dal!.Engineer.Delete(id);//stage 2              

        }
        /// <summary>
        ///get all the details of a dependency, craet a new task and add it to the list of taskdependencys 
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void CreateDependency()
        {
            Console.WriteLine(@$"Please enter the following details about the task you would like to create:
The dependent task");
            if (!int.TryParse(Console.ReadLine(), out int dependTaskNum))
                throw new FormatException("Wrong input");
            Console.WriteLine("The number of the task that needs to be done before:");
            if (!int.TryParse(Console.ReadLine(), out int firstTaskaskNum))
                throw new FormatException("Wrong input");
            Dependency dependence = new(0, dependTaskNum, firstTaskaskNum);
            Console.WriteLine("The id of the dependency is: " + s_dal!.Dependency.Create(dependence));
        }
        /// <summary>
        ///gets a id of a dependency and print the task's details
        /// </summary>
        /// <exception cref="DalWrongInputException">Wrong input</exception>
        static void ReadDependency()
        {
            Console.WriteLine($@"Please enter the id of the dependency you would like to read");
            if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                throw new FormatException("Wrong input");
            Dependency? dependencyToRead = new();
            dependencyToRead = s_dal!.Dependency.Read(dependencyId);
            if (dependencyToRead == null)
                Console.WriteLine($"dependnecy id '{dependencyId}' does not exist");
            else
                Console.WriteLine($@"The dependent task's number is:{dependencyToRead!.DependentTask},
The task depends on task number:{dependencyToRead.DependentOnTask}.");
        }
        /// <summary>
        ///print all the dependencys in the list
        /// </summary>
        static void ReadAllDependencies()
        {
            IEnumerable<Dependency?> dependencies = s_dal!.Dependency.ReadAll();

            foreach (Dependency? dependency in dependencies)//a loop that goes over the list of dependencies
            {
                Console.WriteLine($@"The dependency id is:{dependency?.Id},
The dependent task's number is:{dependency?.DependentTask},
The task depends on task number:{dependency?.DependentOnTask}.
");
            }

        }
        /// <summary>
        ///get all the details of a dependency, check if the new dependency in the list and if yes-change the details
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void UpdateDependency()
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
            s_dal!.Dependency.Update(dependence);
        }
        /// <summary>
        /// deletes the requested dependency
        /// </summary>
        /// <exception cref="FormatException">Wrong input</exception>
        static void DeleteDependency()
        {
            Console.WriteLine($@"Please enter the id of the dependency you would like to delete:");
            if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                throw new FormatException("Wrong input");
            s_dal!.Dependency.Delete(dependencyId);
        }
    }
}

