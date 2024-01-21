using DalApi;
using DO;

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
        elemDependency.Add(elemId);

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
        XElement? elemDependency= dependencyRoot.Elements().FirstOrDefault(s=>(int?)s.Element("Id")==id);
        //XElement? elemDependency;
        //elemDependency =(from item in dependencyRoot.Elements()
        //              where (int?)item.Element("Id")==id
        //             select item).FirstOrDefault();
        if (elemDependency!=null)
           elemDependency.Remove();
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);
    }

    public Dependency? Read(int id)
    {
        dependencyRoot = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elemDependency = dependencyRoot.Elements().FirstOrDefault(s => (int?)s.Element("Id") == id);
        //elemDependency = (from item in dependencyRoot.Elements()
        //                  where (int?)item.Element("Id") == id
        //                  select item).FirstOrDefault();
        if (elemDependency != null)
        {
            return getDependency(elemDependency);
        }
        else
         return null; 


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
        //XElement? elemDependency;
        //elemDependency = (from dependent in dependencyRoot.Elements()
        //                  where (int)dependent.Element("Id")! == item.Id
        //                  select dependent).FirstOrDefault();
        XElement? elemDependency = dependencyRoot.Elements().FirstOrDefault(s => (int?)s.Element("Id") == item.Id);

        elemDependency!.Element("DependentTask")!.Value = Convert.ToString(item.DependentTask);
        elemDependency.Element("DependentOnTask")!.Value = Convert.ToString(item.DependentOnTask);
        XMLTools.SaveListToXMLElement(dependencyRoot, s_dependencys_xml);

    }
    static Dependency getDependency(XElement dependency)
    {

        return new Dependency()
        {
            Id = int.TryParse((string?)dependency.Element("Id"), out var id) ? id : throw new FormatException("can't convert id"),
            DependentTask = int.TryParse((string?)dependency.Element("DependentTask"), out var dependentTask) ? dependentTask : throw new FormatException("can't convert DependentTask"),
            DependentOnTask = int.TryParse((string?)dependency.Element("DependentOnTask"), out var dependentOnTask) ? dependentOnTask : throw new FormatException("can't convert DepenDependentOnTaskdentTask")

        };
        
    }    
    
}
    