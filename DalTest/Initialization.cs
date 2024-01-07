namespace DalTest;
using DalApi;
using DO;
using Microsoft.VisualBasic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

public static class Initialization
{
    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static readonly Random s_rand = new();//The entities will use this random in order to fill the objects values
    private static void createTasks()
    {
        string[] tasksNames =
            {"Analysis","Planning","Design","Prototyping","Testing","Simulation","Integration",
            "Coding","Evaluation","Estimation","Risk","Quality","Compliance","Management","Scheduling",
            "Procurement","Communication","Documentation","Safety","Commissioning"};
        string[] tasksDescription =
            {" Assess project feasibility, risks, and requirements","Define project goals, scope, and objectives.",
            "Develop conceptual and detailed plans for project implementation","Build initial models or prototypes for testing and validation.",
            " Conduct comprehensive tests to ensure functionality and reliability.","Use simulations to model and analyze system behavior",
            "Combine individual components into a unified system.","Write and implement software or programming code.",
            "Assess project performance against predefined criteria"," Estimate project costs, resources, and timelines.",
            "Identify, analyze, and mitigate potential project risks.","Implement measures to ensure high standards of product or service.",
            "Ensure adherence to relevant regulations and standards.","Oversee project activities, resources, and team coordination.",
            "Develop and manage project timelines and milestones.","Acquire necessary materials, equipment, and services.",
            " Facilitate effective information exchange within the team.","Create and maintain project-related documentation.",
            " Implement measures to ensure a safe working environment."," Initiate and oversee the deployment of the project."};
        string[] tasksProduct =
        {

            "Detailed report assessing project feasibility, risks, and requirements","Comprehensive project plan outlining goals, scope, and objectives",
            "Comprehensive design plans for project implementation.",
            " Initial working model or prototype for testing and validation","Documented results of comprehensive tests ensuring functionality.",
            "Detailed model illustrating system behavior under different conditions.","Fully assembled and functional system.",
            "Software or programming code implemented and ready for use.","Assessment of project performance against predefined criteria.",
            "Detailed estimate of project costs, resources, and timelines","Documented strategies to identify, analyze, and mitigate potential risks.",
            "Implementation measures ensuring high standards of the final product.","Documented proof of adherence to relevant regulations and standards.",
            "Overview of project activities, resources, and coordination."," Detailed project timeline with milestones and deadlines",
            "Necessary materials, equipment, and services procured for the project.","Documented plan for effective information exchange within the team.",
            "Comprehensive and organized documentation related to the project.","Documented implementation of safety measures in the project.","Successfully deployed and fully operational project."
        };
        foreach (var _name in tasksNames) foreach (var _description in tasksDescription) foreach (var _product in tasksProduct)
                {
                    double effortTime = s_rand.Next(30, 180);
                    EngineerLevel complex = (EngineerLevel)s_rand.Next((int)EngineerLevel.Beginner, (int)EngineerLevel.Expert);
                    int engineerId = s_rand.Next(200000000, 400000000);
                    DateTime createDateRange = new DateTime(2022, 1, 1);
                    DateTime startDateRange = new DateTime(2025, 1, 1);
                    DateTime finishDateRange = new DateTime(2026, 1, 1);
                    Random gen = new Random();
                    int rangeCreate = (DateTime.Today - createDateRange).Days;
                    DateTime createDate = createDateRange.AddDays(gen.Next(rangeCreate));
                    int rangeStart = (startDateRange - DateTime.Today).Days;
                    DateTime startDate = startDateRange.AddDays(gen.Next(rangeStart));
                    int rangeFinish = (finishDateRange - startDate).Days;
                    DateTime finishDate = finishDateRange.AddDays(gen.Next(rangeFinish));
                    Task newTask = new(_name, _description, 0, _product, complex, engineerId, TimeSpan.FromDays(effortTime), false, finishDate, createDate, startDate, null, null, null);
                    s_dalTask!.Create(newTask);
                }
    }
    private static void createEngineers()
    {
        string[] engineerNames =
        {
        "Daniel Cohen", "Eli levi", "Yair Rosen ",
        "shir Klein", "Dina Hill ", "Shira Stone"
        };
        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(200000000, 400000000);
            while (s_dalEngineer!.Read(_id) != null);
            EngineerLevel _c = (EngineerLevel)s_rand.Next((int)EngineerLevel.Beginner, (int)EngineerLevel.Expert);
            double _cfh = s_rand.Next(150, 1000);
            Engineer newEngineer = new(_id, _name,$"{_name}@gmail.com ", _c , _cfh);
            s_dalEngineer!.Create(newEngineer);
        }
    }
    private static void createDependencys()
    {
        string[] tasksNames =
           {"Analysis","Planning","Design","Prototyping","Testing","Simulation","Integration",
            "Coding","Evaluation","Estimation","Risk","Quality","Compliance","Management","Scheduling",
            "Procurement","Communication","Documentation","Safety","Commissioning"};

        Dependency newDependency = new(0,0,0);
        s_dalDependency!.Create(newDependency);

    }
    public static void  Do()
    {
        IDependency? dalDependency;
        IEngineer? dalEngineer;
        ITask? daITask;

        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_daITask = daITask ?? throw new NullReferenceException("DAL can not be null!");
        CreateEngineers();
    }

    
}
