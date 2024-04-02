namespace Dal;
using DalApi;
using System.Data.Common;
using System.Diagnostics;
using System.Xml.Linq;

/// <summary>
/// interface of the entities in the xml layer
/// </summary>
sealed  internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public IUser User => new UserImplementation();


    public DateTime? StartProject
    {
        set
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            if (root.Element("StartProject") == null)
            {
                XElement start = new("StartProject", value);
                root.Add(start);
            }
            else
                root.Element("StartProject")?.SetValue((value).ToString()!);
            XMLTools.SaveListToXMLElement(root, "data-config");
        }
        get
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            return root.ToDateTimeNullable("StartProject");

        }
    }
    public DateTime? EndProject
    {
        set
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            if (root.Element("EndProject") == null)
            {
                XElement end = new("EndProject", value);
                root.Add(end);
            }
            else
                root.Element("EndProject")?.SetValue((value).ToString()!);
            XMLTools.SaveListToXMLElement(root, "data-config");
        }
        get
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            return root.ToDateTimeNullable("EndProject");

        }
    }


}
