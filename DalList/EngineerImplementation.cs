﻿namespace Dal;
using DalApi;
using DO;

/// <summary>
/// Implementation of the methods for the engineer.
/// </summary>
internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Adding a new object of Engineer to a database, (to the list of objects of Engineers).
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <returns>The method will return the running number of the newly created engineer in the list.</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(Engineer item)
    {
        if(Read(item.Id) !=null)
                throw new DalAlreadyExistException("An enginner type object with such an ID already exists");
        DataSource.Engineers.Add(item);
         return item.Id;
    }
    /// <summary>
    /// The function deletes an existing engineer from the list 
    /// </summary>
    /// <param name="id">ID number of an engineer</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        Engineer? ifExistEngineer = DataSource.Engineers.Find(temp => temp.Id == id);
        if (ifExistEngineer == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={id} does Not exist");
        }
        DataSource.Engineers.Remove(ifExistEngineer);
        return true;
    }
    /// <summary>
    /// Returning a reference to a single object of Engineer with a certain ID.
    /// </summary>
    /// <param name="id">ID number of an engineer.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    public Engineer? Read(int id)
    { 
     return DataSource.Engineers.FirstOrDefault(item => item.Id == id);//stage 2
    }
    /// <summary>
    /// Return a copy of the list of references to all objects of engineer.
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of engineer.</returns>
    //public List<Engineer> ReadAll() stage 1
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }
    /// <summary>
    /// Update of an existing object of enginer.
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Engineer item)
    {
        Engineer? engineer = DataSource.Engineers.Find(Engineer => Engineer.Id == item.Id);

        if (DataSource.Engineers.FirstOrDefault(item) == null)
        {
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        }
        DataSource.Engineers.Remove(engineer!);
        DataSource.Engineers.Add(item);
    }
    /// <summary>
    ///  goes over the list of engineers and return the first engineer in the list on which the filter returns True.
    /// </summary>
    /// <param name="filter">a bool function</param>
    /// <returns>return the first engineer in the list on which the filter returns True</returns>
    public Engineer? Read(Func<Engineer, bool>? filter)//stage 2
    {
        if (filter == null)
        {
            return null;
        }
        else
            return DataSource.Engineers.FirstOrDefault(filter);
    }
    public void Clear() { DataSource.Engineers.Clear(); }

}
