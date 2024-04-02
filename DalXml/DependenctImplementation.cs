using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;
/// <summary>
/// implamanations of dependency in the xml file
/// </summary>
internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";
    XElement dependencyRoot = new XElement("ArrayOfDependency");
    /// <summary>
    /// creats a new dependency in the xml file
    /// </summary>
    /// <param name="item">the dependency to add</param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement elemDependency = new XElement("Dependency");
        int id = Config.NextDependentTaskId;

        XElement elemId = new XElement("Id", id);
        elemDependency.Add(elemId);

        XElement dependentTask = new XElement("DependentTask", item.DependentTask);
        elemDependency.Add(dependentTask);

        XElement dependentOnTask = new XElement("DependentOnTask", item.DependentOnTask);
        elemDependency.Add(dependentOnTask);


        dependencyRoot.Add(elemDependency);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);
        return id;

    }
    /// <summary>
    /// deletes a requested dependency from the xml file
    /// </summary>
    /// <param name="id">the id of the dependency to delete</param>
    public bool Delete(int id)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency = dependencyRoot.Elements().FirstOrDefault(s => (int?)s.Element("Id") == id);
        if (elemDependency != null)
            elemDependency.Remove();
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);
        return true;

    }
    /// <summary>
    /// reads from the xml file a requested dependency
    /// </summary>
    /// <param name="id">the id of th dependency to read</param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency = dependencyRoot.Elements().FirstOrDefault(s => (int?)s.Element("Id") == id);
        if (elemDependency != null)
        {
            return getDependency(elemDependency);
        }
        else
            return null;


    }
    /// <summary>
    /// reads the first dependency fron the xml file that stands under a requested condition
    /// </summary>
    /// <param name="filter">the condition</param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool>? filter)
    {

        return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d)).FirstOrDefault(filter!);
    }
    /// <summary>
    /// returns all the dependencies from the xml file that stands under a condition
    /// </summary>
    /// <param name="filter">the condition</param>
    /// <returns></returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d)).Where(filter!);

        }
        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d));
    }
    /// <summary>
    /// updates a requested dependency in the xml file
    /// </summary>
    /// <param name="item">the dependency to update</param>
    public void Update(Dependency item)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency = dependencyRoot.Elements().FirstOrDefault(s => (int?)s.Element("Id") == item.Id);

        elemDependency!.Element("DependentTask")!.Value = Convert.ToString(item.DependentTask);
        elemDependency.Element("DependentOnTask")!.Value = Convert.ToString(item.DependentOnTask);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);

    }
    /// <summary>
    /// swiches an item from XElement to Dependency
    /// </summary>
    /// <param name="dependency">the dependency in the shape of XElement</param>
    /// <returns>returns the Dependency</returns>
    /// <exception cref="FormatException"></exception>
    static Dependency getDependency(XElement dependency)
    {

        return new Dependency()
        {
            Id = int.TryParse((string?)dependency.Element("Id"), out var id) ? id : throw new FormatException("can't convert id"),
            DependentTask = int.TryParse((string?)dependency.Element("DependentTask"), out var dependentTask) ? dependentTask : throw new FormatException("can't convert DependentTask"),
            DependentOnTask = int.TryParse((string?)dependency.Element("DependentOnTask"), out var dependentOnTask) ? dependentOnTask : throw new FormatException("can't convert DepenDependentOnTaskdentTask")

        };

    }
   public void Clear()
    {
        dependencyRoot= XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        dependencyRoot.RemoveAll();
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);
        Config.NextDependentTaskId = 1;//initialize the running number
    }

}


    