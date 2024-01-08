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
        /// enum for the second menue
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
                    do
                    {
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
                            Engineer? eng = new Engineer();
                            int id = s_dalEngineer.Create(eng);
                            Console.WriteLine("the id of the new engineer is: "+id);
                            break;
                        case SubMenue.Read:
                            break;
                        case SubMenue.ReadAll:
                            List<Engineer> s = s_dalEngineer.ReadAll();
                            break;
                        case SubMenue.Update:
                            break;
                        case SubMenue.Delete:
                            break;
                        default:
                            break;
                    }
                }
                 void  ChoiceDependency()
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
                            break;
                        case SubMenue.Read:
                            break;
                        case SubMenue.ReadAll:
                            List<Dependency> s = s_dalDependency.ReadAll();
                            break;
                        case SubMenue.Update:
                            break;
                        case SubMenue.Delete:
                            break;
                        default:
                            break;
                    }
                }


            }
            catch 
            {

            }
         }

    }
}




