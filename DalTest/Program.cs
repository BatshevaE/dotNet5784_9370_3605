using Dal;
using DalApi;
using DO;
using System.Linq.Expressions;
using System.Threading.Channels;
using System.Xml.Serialization;
using Task = System.Threading.Tasks.Task;

namespace DalTest
{
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
            exit, Task, Engineer, Dependency
        }
        /// <summary>
        /// enum for the sub menue
        /// </summary>
        public enum SubMenue
        {
            Exit, Creat, Read, ReadAll, Update, Delete
        }
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);
                MainMenue choice;
                //The main menue,of the three objects
                void MainMenue()
                {
                    Console.WriteLine(@"Choose one of the following options: 
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choice = (MainMenue)Console.Read();//read the int choice and convert it to MainMenue types
                    switch (choice)
                    {
                        case Program.MainMenue.exit:
                            break;
                        case Program.MainMenue.Task:
                            ChoiceTask();
                            MainMenue();
                            break;
                        case Program.MainMenue.Engineer:
                            ChoiceEngineer();
                            MainMenue();
                            break;
                        case Program.MainMenue.Dependency:
                            ChoiceDependency();
                            MainMenue();
                            break;
                        default:
                            throw new Exception("The selected option does not exist");
                    }

                }
                void ChoiceTask()//A method for the chosen option-task

                {
                    SubMenue choiceTask;
                    Console.WriteLine(@"Choose one of the following options for Task 
                                      Exit:0
                                      Creat:1
                                      Read:2
                                      ReadAll:3
                                      Update:4
                                       Delete:5
                                      ");
                    choiceTask = (SubMenue)Console.Read();//read the int choice and convert it to SubMenue types
                    switch (choiceTask)
                    {
                        case SubMenue.Exit:
                            break;
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
                void ChoiceEngineer()//A method for the chosen option-engineer
                {
                    SubMenue choiceEngineer;
                    Console.WriteLine(@"Choose one of the following options for Engineer:
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choiceEngineer = (SubMenue)Console.Read();//read the int choice and convert it to SubMenue types
                    switch (choiceEngineer)
                    {
                        case SubMenue.Exit:
                            break;
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
                void ChoiceDependency()//A method for the chosen option-dependency
                {
                    SubMenue choiceDependency;
                    Console.WriteLine(@"Choose one of the following options for Dependency: 
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choiceDependency = (SubMenue)Console.Read();//read the int choice and convert it to SubMenue types
                    switch (choiceDependency)
                    {
                        case SubMenue.Exit:
                            break;
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

                void creatTask()//get all the details of a task, craet a new task and add it to the list of tasks
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the task:
                                             Name:");
                        string taskName = Console.ReadLine()!;
                        Console.WriteLine($@"Descriptoin:");
                        string taskDescriptoin = Console.ReadLine()!;
                        Console.WriteLine($@"A task's complex:");
                        EngineerLevel taskComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"A task's product:");
                        string taskProduct = Console.ReadLine()!;
                        Console.WriteLine($@"The engineer's id:");
                        if (!int.TryParse(Console.ReadLine(), out int engineerId))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The engineer's riquired effort time for the task:");
                        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan riquiredEffortTime))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's optional dead line:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime OptionalDeadline))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's create Date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime CreateDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's start date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime StartDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's start task date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime StartTaskDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's actual dead line:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime ActualDeadline))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"Descriptoin:");
                        string? note = Console.ReadLine();

                        DO.Task? task = new DO.Task(taskName, taskDescriptoin, 0, taskProduct, taskComplex, engineerId, CreateDate,riquiredEffortTime, false, OptionalDeadline, StartDate, StartTaskDate, ActualDeadline, note);
                        s_dalTask!.Create(task);//crud
                        Console.WriteLine($"the id of the new task is:{task.Id} ");
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
                void updateTask()//get all the details of a task, check if the new task in the list and if yes-change the details
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the task:
                                             Name:");
                        string taskName = Console.ReadLine()!;
                        Console.WriteLine($@"Descriptoin:");
                        string taskDescriptoin = Console.ReadLine()!;
                        Console.WriteLine($@"A task's complex:");
                        EngineerLevel taskComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"A task's product:");
                        string taskProduct = Console.ReadLine()!;
                        Console.WriteLine($@"The engineer's id:");
                        if (!int.TryParse(Console.ReadLine(), out int engineerId))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The engineer's riquired effort time for the task:");
                        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan riquiredEffortTime))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's optional dead line:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime OptionalDeadline))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's create Date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime CreateDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's start date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime StartDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's start task date:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime StartTaskDate))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"The task's actual dead line:");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime ActualDeadline))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"note:");
                        string? note = Console.ReadLine();
                        DO.Task? task = new DO.Task(taskName, taskDescriptoin, 0, taskProduct, taskComplex, engineerId, CreateDate, riquiredEffortTime, false, OptionalDeadline, StartDate, StartTaskDate, ActualDeadline, note);
                        s_dalTask!.Update(task);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
                void deleteTask()//get id of task and delete the task with this id from the list
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the task you would like to delete from the list:");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                            throw new FormatException("Wrong input");
                        s_dalTask!.Delete(id);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
                void readTask()//gets a id of a task and print the task's details
                {
                    try
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
                                               The task's product is:{taskToRead.Product},
                                               The task's engineer id is:{taskToRead.Engineerid}.
                                               The task's riquired effort time  is:{taskToRead.RiquiredEffortTime}.
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
                    catch (FormatException ex) { Console.WriteLine(ex); };

                }
                void readListTasks()//print all the tasks in the list
                {
                    try
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
                                               The task's product is:{task.Product},
                                               The task's engineer id is:{task.Engineerid}.
                                               The task's riquired effort time  is:{task.RiquiredEffortTime}.
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
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
                void createEngineer()//get all the details of an engineer, craet a new task and add it to the list of engineer
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the engineer:
                                             Name:");
                        string? engineerName = Console.ReadLine();
                        Console.WriteLine($@"Id:");

                        if (!int.TryParse(Console.ReadLine(), out int engineerId))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"Cost for an hour:");
                        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
                            throw new FormatException("Wrong input");

                        Console.WriteLine($@"Complex of the engineer:");
                        EngineerLevel engineerComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"An Email address:");
                        string? engineerEmail = Console.ReadLine();
                        Engineer engineer = new Engineer(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                        s_dalEngineer!.Create(engineer);
                        Console.WriteLine($"the id of the new engineer is:{engineerId} ");
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
                void readEngineer()//gets a id of an engineer and print the task's details
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the engineer you would like to read:");
                        if (!int.TryParse(Console.ReadLine(), out int engineerId))
                            throw new FormatException("Wrong input");
                        Engineer? engineerToRead = new Engineer();
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
                    catch (FormatException ex) { Console.WriteLine(ex); }
                    catch (Exception ex) { Console.WriteLine(ex); };

                }
                void readAllEngineers()//print all the engineers in the list
                {
                    try
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

                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void updateEngineer()//get all the details of an engineer, check if the new engineer in the list and if yes-change the details
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the engineer you would like to update:
                                             Name:");
                        string? engineerName = Console.ReadLine();
                        Console.WriteLine($@"Id:");
                        if (!int.TryParse(Console.ReadLine(), out int engineerId))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"Cost for an hour:");
                        if (!double.TryParse(Console.ReadLine(), out double engineerCost))
                            throw new FormatException("Wrong input");
                        Console.WriteLine($@"Complex of the engineer:");
                        EngineerLevel engineerComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"An Email address:");
                        string? engineerEmail = Console.ReadLine();
                        Engineer engineer = new Engineer(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                        s_dalEngineer!.Update(engineer);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); }
                }
                void deleteEngineer()//get id of engineer and delete the engineer with this id from the list
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                            throw new FormatException("Wrong input");
                        s_dalEngineer!.Delete(id);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); }
                }
                void createDependency()//get all the details of a dependency, craet a new task and add it to the list of taskdependencys
                {
                    try
                    {
                        Console.WriteLine(@$"Please enter the following details about the task you would like to create:
                                         The dependent task");
                        if (!int.TryParse(Console.ReadLine(), out int dependTaskNum))
                            throw new FormatException("Wrong input");
                        Console.WriteLine("The number of the task that needs to be done before:");
                        if (!int.TryParse(Console.ReadLine(), out int firstTaskaskNum))
                            throw new FormatException("Wrong input");
                        Dependency dependence = new Dependency(0, dependTaskNum, firstTaskaskNum);
                        Console.WriteLine("The id of the dependency is: " + s_dalDependency!.Create(dependence));
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); }
                }
                void readDependency()//gets a id of a dependency and print the task's details
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the dependency you would like to read");
                        if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                            throw new FormatException("Wrong input");

                        Dependency? dependencyToRead = new Dependency();
                        dependencyToRead = s_dalDependency!.Read(dependencyId);
                        if (dependencyToRead == null)
                            throw new Exception("The dependency with the requested id wasn't found in the list");
                        else
                        {
                            Console.WriteLine($@"The dependent task's number is:{dependencyToRead.DependentTask},
                                                 The task depends on task number:{dependencyToRead.DependentOnTask}.");
                        }

                    }
                    catch (FormatException ex) { Console.WriteLine(ex); }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
                void readAllDependencies()//print all the dependencys in the list
                {
                    try
                    {
                        List<Dependency> dependencies = s_dalDependency!.ReadAll();
                        if (dependencies.Count == 0)
                            throw new Exception("The list of dependencies is empty");
                        else
                        {
                            foreach (Dependency dependency in dependencies)//a loop that goes over the list of dependencies
                            {
                                Console.WriteLine($@"The dependent task's number is:{dependency.DependentTask},
                                                     The task depends on task number:{dependency.DependentOnTask}.");
                            }
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void updateDependency()//get all the details of a dependency, check if the new dependency in the list and if yes-change the details
                {
                    try
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
                        Dependency dependence = new Dependency(dependencyId, dependTaskNum, firstTaskaskNum);
                        s_dalDependency!.Update(dependence);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
                void deleteDependency()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the dependency you would like to delete:");
                        if (!int.TryParse(Console.ReadLine(), out int dependencyId))
                            throw new FormatException("Wrong input");
                        s_dalDependency!.Delete(dependencyId);
                    }
                    catch (FormatException ex) { Console.WriteLine(ex); };
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }
    }
}




