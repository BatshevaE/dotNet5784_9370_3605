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
    /// This method will schedule the private methods we prepared and start the initialization of the lists.
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
            {"Analysis"/*1*/,
             "Planning"/*2*/,
             "Design"/*3*/,
             "Prototyping"/*4*/,
             "Testing"/*5*/,
             "Simulation"/*6*/,
             "Integration"/*7*/,
             "Coding"/*8*/,
             "Evaluation"/*9*/,
             "Estimation"/*10*/,
             "Risk"/*11*/,
             "Quality"/*12*/,
             "Compliance"/*13*/,
             "Management"/*14*/,
             "Scheduling"/*15*/,
             "Procurement"/*16*/,
             "Communication"/*17*/,
             "Documentation"/*18*/,
             "Safety"/*19*/,
             "Commissioning"/*20*/,
             "Material"/*21*/,
             "System"/*22*/,
             "Software"/*23*/,
             "Budgeting"/*24*/,
             "Procurement"/*25*/,
             "Mitigation"/*26*/,
             "Assurance"/*27*/,
             "Approvals"/*28*/,
             "Timeline"/*29*/,
             "Supplier"/*30*/,
             "Collaboration"/*31*/,
             "Stakeholder"/*32*/,
             "Ergonomics"/*33*/,
             "Final"/*34*/,
             "Technical"/*35*/,
             "Commissioning"/*36*/};
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
            " Implement measures to ensure a safe working environment."," Initiate and oversee the deployment of the project.",
            " Developing a system to manage and track materials used in a project, including inventory, procurement, and usage",
            "Designing and implementing an integrated system architecture for a specific project or business process.",
            "Developing an application or program to address a specific need or perform certain tasks.",
            "Creating and managing the financial plan for a project, including cost estimation, allocation, and tracking.",
            "Managing the process of purchasing goods or services required for the project.",
            "Identifying and addressing potential risks or threats to the project's success.",
            "Ensuring the quality and compliance of deliverables with specified standards and requirements.",
            "Managing the process of obtaining necessary permissions or authorizations for project milestones or changes.",
            "Creating and managing a project schedule outlining key milestones and activities.",
            "Managing the selection, relationship, and procurement of goods or services from external vendors or suppliers to support the project's needs.",
            "Facilitating effective communication, cooperation, and information sharing among project team members and external stakeholders.",
            "Managing relationships and communication with individuals or groups affected by or interested in the project.",
            "Designing systems or interfaces to optimize user comfort, efficiency, and safety.",
            "Preparing the project for completion, finalizing deliverables, and conducting post-project assessments.",
            "Addressing the technical aspects and requirements of the project, including design, development, implementation, and maintenance of technical solutions.",
            "The process of ensuring that all systems and components of the project are designed, installed, tested, operated, and maintained according to the operational requirements."};
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
            "Comprehensive and organized documentation related to the project.","Documented implementation of safety measures in the project.","Successfully deployed and fully operational project.",
            "A database-driven software or application that allows for easy input, tracking, and analysis of materials, their quantities, suppliers, and utilization within the project",
            "An efficient, scalable, and well-documented system architecture that meets the project's requirements, including hardware, software, networking components, and protocols.",
            "A functional software application that meets defined specifications, with well-documented code and user-friendly interfaces, catering to the intended user base.",
            "A comprehensive budget document outlining expenses, resource allocation, timelines, and potential financial risks, often managed through specialized software or spreadsheets.",
            "A procurement strategy outlining vendor selection criteria, purchase orders, contracts, and a streamlined process for acquiring necessary resources or services.",
            "A risk mitigation plan outlining identified risks, their impact, and strategies to minimize or eliminate these risks, ensuring project continuity.",
            "Quality assurance protocols, test plans, and compliance documentation demonstrating that project deliverables meet or exceed defined criteria.",
            "A documented approval workflow indicating responsible parties, timelines, and required signatures or endorsements for project phases or modifications.",
            "A detailed project timeline with tasks, dependencies, start and end dates, critical paths, and Gantt charts, providing a visual representation of the project's progress.",
            "A structured supplier management system comprising:Supplier Database, Selection Criteria, Contracts and Agreements, Performance Evaluation, Risk Mitigation Plan",
            "An efficient collaboration framework that includes:Communication Channels, Roles and Responsibilities, Collaboration Tools, Feedback Mechanisms, Conflict Resolution Strategies",
            "A stakeholder engagement plan outlining strategies to involve and communicate with stakeholders, addressing their concerns, expectations, and feedback throughout the project lifecycle.",
            "An ergonomic design plan ensuring that workstations, interfaces, or tools utilized in the project are tailored to support user comfort, productivity, and health.",
            "Completed project deliverables, documentation, post-implementation review reports, and any necessary handover materials for ongoing maintenance or future reference.",
            "A comprehensive technical plan encompassing:Technical Specifications, Development Process, Testing Protocols, Documentation and Manuals",
            " A comprehensive commissioning plan incorporating:Testing and Verification, Operational Readiness, Handover and Acceptance, Post-Commissioning Support"};
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
                    Task newTask = new(_name,tasksDescription[i++], 0,tasksProduct[i++], complex, engineerId, TimeSpan.FromDays(effortTime), false, finishDate, createDate, startDate, null, null, null);//ctor
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
        Dependency[] newDependency = {

            new Dependency(0,1,2),
            new Dependency(0,3,2),
            new Dependency(0,6,1),
            new Dependency(0,11,1),
            new Dependency(0,7,3),
            new Dependency(0,4,3),
            new Dependency(0,5,4),
            new Dependency(0,5,4),
            new Dependency(0,21,4),
            new Dependency(0,12,5),
            new Dependency(0,9,5),
            new Dependency(0,7,6),
            new Dependency(0,8,6),
            new Dependency(0,8,7),
            new Dependency(0,22,7),
            new Dependency(0,5,8),
            new Dependency(0,23,8),
            new Dependency(0,13,9),
            new Dependency(0,18,9),
            new Dependency(0,24,10),
            new Dependency(0,25,10),
            new Dependency(0,26,11),
            new Dependency(0,12,11),
            new Dependency(0,13,12),
            new Dependency(0,27,12),
            new Dependency(0,28,13),
            new Dependency(0,18,13),
            new Dependency(0,15,14),
            new Dependency(0,25,14),
            new Dependency(0,29,15),
            new Dependency(0,17,15),
            new Dependency(0,21,25),
            new Dependency(0,30,25),
            new Dependency(0,31,17),
            new Dependency(0,32,17),
            new Dependency(0,19,18),
            new Dependency(0,33,19),
            new Dependency(0,9,19),
            new Dependency(0,35,18),
            new Dependency(0,34,36),
            new Dependency(0,5,36)
        };
    
    foreach(Dependency dependency in newDependency)
            s_dalDependency!.Create(dependency);
    }
   
   
}
