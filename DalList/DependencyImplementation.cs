﻿
namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Implementation of the methods for the dependency list. 
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Adding a new object of dependecy  to a database, (to the list of objects of dependecys).
    /// </summary>
    /// <param name="item">A reference to an existing object of dependency.</param>
    /// <returns>The method will return the running number of the newly created dependency in the list.</returns>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependentTaskId;//A new ID number with the value of the next running number.
        Dependency copy = item with { Id = id };//creating a copy of item withe the new id
        DataSource.Dependencys.Add(copy);//add the new copy to the list of dependecys
        return id;
    }
    /// <summary>
    /// The function deletes an existing Dependency from the list 
    /// </summary>
    /// <param name="id">ID number of a dependecy</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        Dependency? ifExistDependency = DataSource.Dependencys.Find(temp => temp.Id == id);
        if (ifExistDependency == null)
        {
            throw new DalDoesNotExistException($"Dependent with ID={id} does Not exist");
        }
        DataSource.Dependencys.Remove(ifExistDependency);
        return true;

    }
    /// <summary>
    /// Returning a reference to a single object of dependency with a certain ID.
    /// </summary>
    /// <param name="id">ID number of a dependecy</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing dependecy.
    ///Otherwise, the method will return null.</returns>
    public Dependency? Read(int id)
    {
        if (DataSource.Dependencys.FirstOrDefault(item => item.Id == id) != null)//if there is a dependecy with the given id
            return DataSource.Dependencys.FirstOrDefault(item => item.Id == id);//return this dependecy
        //else
        return null;
    }
    /// <summary>
    /// Return a copy of the list of references to all objects dependecy
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of dependecy.</returns>
    //public List<Dependency> ReadAll()//stage 1
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencys
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencys
               select item;
    }
    /// <summary>
    /// Update of an existing object of dependect.
    /// </summary>
    /// <param name="item">A reference to an existing object of dependect</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Dependency item)
    {
        Dependency? dependency = DataSource.Dependencys.Find(Dependency => Dependency.Id == item.Id);

        if (DataSource.Dependencys.FirstOrDefault(item) == null)//if item wasnt found in the list of dependecys
        {
            throw new DalDoesNotExistException($"Dependent with ID={item.Id} does Not exist");
        }
        DataSource.Dependencys.Remove(dependency!);//remove the found dependent
        DataSource.Dependencys.Add(item);
    }
    /// <summary>
    /// goes over the list of dependencys and return the first dependency in the list on which the filter returns True.
    /// </summary>
    /// <param name="filter">a bool function</param>
    /// <returns>return the first dependency in the list on which the filter returns True.</returns>
    public Dependency? Read(Func<Dependency, bool>? filter)//stage 2
    {
        if (filter == null)
        {
            return null;
        }
        else
        return DataSource.Dependencys.FirstOrDefault(filter);
    }
   public void Clear() { DataSource.Dependencys.Clear(); }
   
}


