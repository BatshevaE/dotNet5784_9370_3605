namespace DalTest;
using DalApi;
using DO;

public static class Initialization
{
    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static readonly Random s_rand = new();//The entities will use this random in order to fill the objects values
    /// <summary>
    /// This method will schedule the private methods we prepared and trigger the initialization of the lists.
    /// </summary>
    /// <param name="dalTask">The access variables of task</param>
    /// <param name="dalEngineer">The access variables of engineer</param>
    /// <param name="dalDependency">The access variables of dependency</param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do(ITask? dalTask, IEngineer? dalEngineer, IDependency? dalDependency)
    {
        ITask? s_dalTask;
        IDependency? s_dalDependency;
        IEngineer? s_dalEngineer;
       
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");

        createTasks();
        createDependencys();
        createEngineers();

    }
    /// <summary>
    /// a method to initialize the list of tasks
    /// </summary>
    private static void createTasks()
    {
        //an array of names
        string[] tasksNames =
            {"Analysis","Planning","Design","Prototyping","Testing","Simulation","Integration",
            "Coding","Evaluation","Estimation","Risk","Quality","Compliance","Management","Scheduling",
            "Procurement","Communication","Documentation","Safety","Commissioning"};
        //an array of descriptions
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
        //an array of products
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
        foreach (var _name in tasksNames)//goes over the array of names foreach (var _description in tasksDescription) foreach (var _product in tasksProduct)
                {
                    int i = 0;
                    double effortTime = s_rand.Next(30, 180);//here we get a random time in days for each task
                    EngineerLevel complex = (EngineerLevel)s_rand.Next((int)EngineerLevel.Beginner, (int)EngineerLevel.Expert);//here we get a random complex for each task 
                    int engineerId = s_rand.Next(200000000, 400000000);//here we get a random id for engineer for the task
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
                    Task newTask = new(_name,/* _description*/tasksDescription[i++], 0, /* _product*/ tasksProduct[i++], complex, engineerId, TimeSpan.FromDays(effortTime), false, finishDate, createDate, startDate, null, null, null);//ctor
                    s_dalTask!.Create(newTask);
                    //DateTime createDate=DateTime.Today;
                    //DateTime finishDate=null;
                    //DateTime DateTime startDate = null;

                }
    }
    /// <summary>
    /// a method to initialize the list of Engineers
    /// </summary>
    private static void createEngineers()
    {
        //array of names of engineers
        string[] engineerNames =
        {
        "Daniel Cohen", "Eli levi", "Yair Rosen ",
        "shir Klein", "Dina Hill ", "Shira Stone"
        };
        foreach (var _name in engineerNames)//a loop that going over the array
        {
            int _id;
            do
                _id = s_rand.Next(200000000, 400000000);//here we get a random id for the engineer
            while (s_dalEngineer!.Read(_id) != null);
            EngineerLevel _c = (EngineerLevel)s_rand.Next((int)EngineerLevel.Beginner, (int)EngineerLevel.Expert);//here we get a random complex level of the engineer
            double _cfh = s_rand.Next(150, 1000);//a random cost for hour of the engineer
            Engineer newEngineer = new(_id, _name,$"{_name}@gmail.com ", _c , _cfh);//ctor

            s_dalEngineer!.Create(newEngineer);
        }
    }
    /// <summary>
    /// a method to initialize the list of Dependencys
    /// </summary>
    private static void createDependencys()

    { 
        Dependency newDependency = new(0,0,0);
        s_dalDependency!.Create(newDependency);

    }
   
   
}
