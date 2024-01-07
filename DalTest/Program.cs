using Dal;
using DalApi;
using DO;
using System.Linq.Expressions;
using System.Threading.Channels;
using System.Xml.Serialization;

namespace DalTest
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        public enum MainMenue
        {
            exit, Task, Engineer, Dependency
        }
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
                                      Task:1
                                      Engineer:2
                                      Dependency:3");
                    choiceTask = (SubMenue)Console.Read();
                    switch (choiceTask)
                    {
                        case SubMenue.Exit:
                            break;
                        case SubMenue.Creat:
                            break;
                        case SubMenue.Read:
                            break;
                        case SubMenue.ReadAll:
                            break;
                        case SubMenue.Update:
                            break;
                        case SubMenue.Delete:
                            break;
                        default:
                            break;
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




