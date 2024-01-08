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
            Exit,Creat,Read,ReadAll,Update,Delete
        }
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);
                MainMenue choice;
                void MainMenue()
                {
                    //  do
                    // {
                    Console.WriteLine(@"Choose one of the following options: 
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choice = (MainMenue)Console.Read();
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
                    //  }
                    // while (choice != 0);

                }
                void ChoiceTask()
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
                    choiceTask = (SubMenue)Console.Read();
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
                void creatTask()
                {

                }
                void updateTask()
                {

                }

                void deleteTask()
                {

                }
                void readTask()
                {
                    Console.WriteLine("Please enter a task's id");
                    string? id = Console.ReadLine();
                    int v = int.Parse(id!);    
                    Task? task = s_dalTask.Read();
                }
                void readListTasks()
                {
                    List<Task> listTasks = s_dalTask.ReadAll();
                    Console.WriteLine("The tasks are:");
                    foreach (Task _task in listTasks) 
                    {
                        Console.WriteLine(_task.Name + " : " + _task.Descriptoin+ " " + _task.Id);
                    }


                }
                void ChoiceEngineer()
                {
                    SubMenue choiceEngineer;
                    Console.WriteLine(@"Choose one of the following options for Engineer:
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choiceEngineer = (SubMenue)Console.Read();
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
                void createEngineer()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the engineer:
                                             Name:");
                        string? engineerName = Console.ReadLine();
                        Console.WriteLine($@"Id:");
                        int engineerId = Console.Read();
                        Console.WriteLine($@"Cost for an hour:");
                        double engineerCost = Console.Read();
                        Console.WriteLine($@"Complex of the engineer:");
                        EngineerLevel engineerComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"An Email address:");
                        string? engineerEmail = Console.ReadLine();
                        Engineer engineer = new Engineer(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                        s_dalEngineer!.Create(engineer);
                        Console.WriteLine($"the id of the new engineer is:{engineerId} ");
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void readEngineer()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the engineer you would like to read:");
                        int engineerId = Console.Read();
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
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void readAllEngineers()
                {
                    try
                    {
                        List<Engineer> engineers = s_dalEngineer!.ReadAll();
                        if (engineers.Count == 0)
                            throw new Exception("The list of engineers is empty");
                        else
                        {
                            foreach (Engineer engineer in engineers)
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
                void updateEngineer()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the following details about the engineer you would like to update:
                                             Name:");
                        string? engineerName = Console.ReadLine();
                        Console.WriteLine($@"Id:");
                        int engineerId = Console.Read();
                        Console.WriteLine($@"Cost for an hour:");
                        double engineerCost = Console.Read();
                        Console.WriteLine($@"Complex of the engineer:");
                        EngineerLevel engineerComplex = (EngineerLevel)Console.Read();
                        Console.WriteLine($@"An Email address:");
                        string? engineerEmail = Console.ReadLine();
                        Engineer engineer = new Engineer(engineerId, engineerName, engineerEmail, engineerComplex, engineerCost);
                        s_dalEngineer!.Update(engineer);
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void deleteEngineer()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the engineer you would like to delete from the list:");
                        int id = Console.Read();
                        s_dalEngineer!.Delete(id);
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void ChoiceDependency()
                {
                    SubMenue choiceDependency;
                    Console.WriteLine(@"Choose one of the following options for Dependency: 
                                      Exit:0
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choiceDependency = (SubMenue)Console.Read();
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
                void createDependency()
                {
                    Console.WriteLine(@$"Please enter the following details about the task you would like to create:
                                         The dependent task");
                    int dependTaskNum = Console.Read();
                    Console.WriteLine("The number of the task that needs to be done before:");
                    int firstTaskaskNum = Console.Read();
                    Dependency dependence=new Dependency(0,dependTaskNum,firstTaskaskNum);
                    Console.WriteLine(s_dalDependency!.Create(dependence));
                    
                }
               void readDependency()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the dependency you would like to read");
                        int dependencyId = Console.Read();
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
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
                   void readAllDependencies()
                  {
                    try
                    {
                        List<Dependency> dependencies = s_dalDependency!.ReadAll();
                        if (dependencies.Count == 0)
                            throw new Exception("The list of dependencies is empty");
                        else
                        {
                            foreach (Dependency dependency in dependencies)
                            {
                                Console.WriteLine($@"The dependent task's number is:{dependency.DependentTask},
                                                     The task depends on task number:{dependency.DependentOnTask}.");
                            }
                        }
                    }
                    catch (Exception ex){ Console.WriteLine(ex); };
                  }
                void updateDependency()
                {
                    try 
                    {
                        Console.WriteLine(@$"Please enter the following details about the task you would like to update:
                                             The dependent's id:");
                        int dependencyId = Console.Read();
                        Console.WriteLine(@$"The dependent task's number:");
                        int dependTaskNum = Console.Read();
                        Console.WriteLine("The number of the task that needs to be done before:");
                        int firstTaskaskNum = Console.Read();
                        Dependency dependence = new Dependency(dependencyId, dependTaskNum, firstTaskaskNum);
                        s_dalDependency!.Update(dependence);
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
                void deleteDependency()
                {
                    try
                    {
                        Console.WriteLine($@"Please enter the id of the dependency you would like to delete:");
                        int dependencyId = Console.Read();
                        s_dalDependency!.Delete(dependencyId);
                    }
                    catch (Exception ex) { Console.WriteLine(ex); };
                }
            }
            catch
            {

            }
         }

    }
}




