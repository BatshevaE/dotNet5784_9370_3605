namespace DalApi;

using System.Collections.Generic;
using System.Xml.Linq;
/// <summary>
/// a class which knows how to read the configuration file at runtime
/// </summary>
static class Config
{
    /// <summary>
    /// internal PDS class
    /// </summary>
    internal record DalImplementation
    (string Package,   // package/dll name
     string Namespace, // namespace where DAL implementation class is contained in
     string Class   // DAL implementation class name
    );

    internal static string s_dalName;
    internal static Dictionary<string, DalImplementation> s_dalPackages;

    static Config()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") ??
  throw new DalConfigException("dal-config.xml file is not found");

        s_dalName =//Brings an object of an element with the name "dal" from the DOM tree,
           dalConfig.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");  //and the Value attribute brings the value inside the element


        var packages = dalConfig.Element("dal-packages")?.Elements() ??
  throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = (from item in packages
                         let pkg = item.Value
                         let ns = item.Attribute("namespace")?.Value ?? "Dal"
                         let cls = item.Attribute("class")?.Value ?? pkg
                         select (item.Name, new DalImplementation(pkg, ns, cls))
                        ).ToDictionary(p => "" + p.Name, p => p.Item2); //Constructs and returns a hashed table whose sub-element name is a hash key,
                                                                        //and the element value is the value attached to the hash key
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
