using DalApi;
using DO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";
    XElement dependencyRoot = new XElement("ArrayOfDependency");
    //dependencyRoot.Save();
    public int Create(Dependency item)
    {
        dependencyRoot=XMLTools.LoadListFromXMLElement(s_dependencys_xml);    
        XElement elemDependency = new XElement("Dependency");
        int id = Config.NextDependentTaskId;

        XElement elemId =new XElement("Id",id);
        elemDependency.Add(id);

        XElement dependentTask = new XElement("DependentTask", item.DependentTask) ;
        elemDependency.Add(dependentTask);

        XElement dependentOnTask = new XElement("DependentOnTask", item.DependentOnTask);
        elemDependency.Add(dependentOnTask);

        
        dependencyRoot.Add(elemDependency);
        XMLTools.SaveListToXMLElement(dependencyRoot,s_dependencys_xml);
        return id;

    }

    public void Delete(int id)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency;
        elemDependency=(from item in dependencyRoot.Elements()
                        where Convert.ToInt32(item.Element("Id")!.Value)==id
                        select item).FirstOrDefault();
        if(elemDependency!=null)
           elemDependency.Remove();
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);
    }

    public Dependency? Read(int id)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency;
        elemDependency = (from item in dependencyRoot.Elements()
                          where Convert.ToInt32(item.Element("Id")!.Value) == id
                          select item).FirstOrDefault();

        return new Dependency
        {
            Id = id,
            DependentTask =int.TryParse((string?)elemDependency?.Element("DependentTask"), out var DependentTask) ? DependentTask : 0,
            DependentOnTask = int.TryParse((string?)elemDependency?.Element("DependentOnTask"), out var DependentOnTask) ? DependentOnTask : 0
        };
    }

    public Dependency? Read(Func<Dependency, bool>? filter)
    {

       return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d=>getDependency(d)).FirstOrDefault(filter!);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d)).Where(filter!);

        }
        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d));
    }

    public void Update(Dependency item)
    {
        dependencyRoot= XMLTools.LoadListFromXMLElement(s_dependencys_xml);    
        XElement? elemDependency;
        elemDependency = (from dependent in dependencyRoot.Elements()
                          where Convert.ToInt32(dependent.Element("Id")!.Value) == item.Id
                          select dependent).FirstOrDefault();
        
        elemDependency!.Element("DependentTask")!.Value = Convert.ToString(item.DependentTask);
        elemDependency.Element("DependentOnTask")!.Value = Convert.ToString(item.DependentOnTask);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);

    }
    static Dependency getDependency(XElement dependency)
    {

        int id = Convert.ToInt32(dependency.Element("Id")!.Value);
        int dependentTask = Convert.ToInt32(dependency.Element("DependentTask")!.Value);
        int dependentOnTask = Convert.ToInt32(dependency.Element("DependentOnTask")!.Value);
       
        return new Dependency(id, dependentTask, dependentOnTask);
        
    }
}
