﻿namespace Dal;
using DalApi;
using DO;
/// <summary>
/// the implemenations of Engineer in the xml file
/// </summary>
internal class EngineerImplementation:IEngineer
{
    readonly string s_engineers_xml = "engineers";
    /// <summary>
    /// creats engineer in the xml file
    /// </summary>
    /// <param name="item">the engineer to add to the xml file</param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (Read(item.Id) != null)
            throw new DalAlreadyExistException("An enginner type object with such an ID already exists");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

        return item.Id;
    }
    /// <summary>
    /// The function deletes an existing engineer from the xml file of engineers
    /// </summary>
    /// <param name="id">ID number of an engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineer? ifExistEngineer = Engineers.Find(temp => temp.Id == id);
        if (ifExistEngineer == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        }
        Engineers.Remove(ifExistEngineer);
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
        return true;
    }
    /// <summary>
    /// Returning a reference to a single object of Engineer with a certain ID from the xml file.
    /// </summary>
    /// <param name="id">ID number of an engineer.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    public Engineer? Read(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? eng= Engineers.FirstOrDefault(item => item.Id == id);//stage 2
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
        return eng;
    }
    /// <summary>
    /// Return a copy of the list of references to all objects of engineer from the xml file.
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of engineer.</returns>
    //public List<Engineer> ReadAll() stage 1
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (filter != null)
        {
            XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
            return from item in Engineers
                   where filter(item)
                   select item;
        }
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
        return from item in Engineers
               select item;
    }
    /// <summary>
    /// Update of an existing object of enginer in the xml file.
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? engineer = Engineers.Find(Engineer => Engineer.Id == item.Id);
        if (Engineers.FirstOrDefault(item) == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        }
        Engineers.Remove(engineer!);
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
    }
    /// <summary>
    ///  goes over the xml file of engineers and return the first engineer in the list on which the filter returns True.
    /// </summary>
    /// <param name="filter">a bool function</param>
    /// <returns>return the first engineer in the list on which the filter returns True</returns>
    public Engineer? Read(Func<Engineer, bool>? filter)//stage 2
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (filter == null)
        {
            XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

            return null;
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
            return Engineers.FirstOrDefault(filter);
        }
    }
    /// <summary>
    /// deletes all the Engineers from the xml file
    /// </summary>
    public void Clear()
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineers.Clear();
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
    }
}
