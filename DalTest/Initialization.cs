namespace DalTest;
using DalApi;
using DO;
using System.Xml.Linq;

public static class Initialization
{
    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static readonly Random s_rand = new();//The entities will use this random in order to fill the objects values
    private static void createEngineers()
    {
        string[] engineerNames =
        {
        "Daniel Cohen", "Eli levi", "Yair Rosen ",
        "shir Klein", "Dina Hill ", "Shira Stone"
    };
        string[] engineerEmailAdress =
       {
        "DanielC@gmail.com", "Elil@gmail.com", "YairR@gmail.com ",
        "shirK@gmail.com", "DinaH@gmail.com ", "ShiraS@gmail.com"
    };

        foreach (var _name in engineerNames)
        {
            int _id,x=0;
            do
                _id = s_rand.Next(200000000, 400000000);
            while (s_dalEngineer!.Read(_id) != null);

            EngineerLevel _c = (EngineerLevel)s_rand.Next((int)EngineerLevel.Beginner, (int)EngineerLevel.Expert);

            
            double _cfh = s_rand.Next(150, 1000);
            string? _ema= engineerEmailAdress[x];

            Engineer newEngineer = new(_id, _name, _ema, _c , _cfh);

            s_dalEngineer!.Create(newEngineer);
        }
    }
    private static void createDependencys()
    {
        //foreach (var _name in s_dalTask)
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
