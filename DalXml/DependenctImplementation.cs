using DalApi;
using DalXml;
using DO;
using System.Linq;
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
                        where Convert.ToInt32(item.Element("id")!.Value)==id
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
                          where Convert.ToInt32(item.Element("id")!.Value) == id
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
        List<Dependency> Dependency = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencys_xml);

        if (filter == null)
        {
            XMLTools.SaveListToXMLSerializer(Dependency, s_dependencys_xml);
            return null;
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(Dependency, s_dependencys_xml);

            return Dependency.FirstOrDefault(filter);
        }

    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<Dependency> Dependency = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencys_xml);

        if (filter != null)
        {
            XMLTools.SaveListToXMLSerializer(Dependency, s_dependencys_xml);
            return from item in Dependency
                   where filter(item)
                   select item;

        }
        XMLTools.SaveListToXMLSerializer(Dependency, s_dependencys_xml);
        return from item in Dependency
               select item;
    }

    public void Update(Dependency item)
    {
        dependencyRoot= XMLTools.LoadListFromXMLElement(s_dependencys_xml);    
        XElement? elemDependency;
        elemDependency = (from dependent in dependencyRoot.Elements()
                          where Convert.ToInt32(dependent.Element("id")!.Value) == item.Id
                          select dependent).FirstOrDefault();
        
        elemDependency!.Element("dependentTask")!.Value = Convert.ToString(item.DependentTask);
        elemDependency.Element("dependentOnTask")!.Value = Convert.ToString(item.DependentOnTask);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);

    }
}
