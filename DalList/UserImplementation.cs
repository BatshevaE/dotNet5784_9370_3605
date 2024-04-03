using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class UserImplementation:IUser
{
    /// <summary>
    /// Adding a new object of Engineer to a database, (to the list of objects of users).
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <returns>The method will return the running number of the newly created engineer in the list.</returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(User item)
    {
        if (!(ReadAll().Any()) || ReadAll().Count() == 0) { User us = new(item.Password, item.Name, true, item.Id); DataSource.Users.Add(item);return item.Password; }
        if (Read(item.Password) != null)
            throw new DalAlreadyExistException("An user type object with such Password already exists");
        if((DataSource.Engineers.FirstOrDefault(t=>(t.Name==item.Name)||( t.Id == item.Id)) ==null)&&(item.Password!=111)&&(item.Password!=100))
            throw new DalDoesNotExistException("Such User can't be assigend to the system");
        DataSource.Users.Add(item);
        return item.Password;
    }
    /// <summary>
    /// The function deletes an existing user from the list 
    /// </summary>
    /// <param name="id">ID number of an engineer</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int Password)
    {
        User? ifUser = DataSource.Users.Find(temp => temp.Password == Password);
        if (ifUser == null)
        {
            throw new DalDoesNotExistException($"User with Password={Password} does Not exist");
        }
        DataSource.Users.Remove(ifUser);
        return true;
    }
    /// <summary>
    /// Returning a reference to a single object of Engineer with a certain ID.
    /// </summary>
    /// <param name="id">ID number of an user.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    public User? Read(int password)
    {
        return DataSource.Users.FirstOrDefault(item =>  (item.Password == password));//stage 2
        ;//stage 2
    }
    /// <summary>
    /// Return a copy of the list of references to all objects of users.
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of engineer.</returns>
    //public List<Engineer> ReadAll() stage 1
    public IEnumerable<User> ReadAll(Func<User, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Users
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Users
               select item;
    }
    /// <summary>
    /// Update of an existing object of enginer.
    /// </summary>
    /// <param name="item">A reference to an existing object of user.</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(User item)
    {
        User? user = DataSource.Users.Find(User => User.Password == item.Password);

        if (DataSource.Users.FirstOrDefault(item) == null)
        {
            throw new DalDoesNotExistException($"User with Password={item.Password} does Not exist");
        }
        DataSource.Users.Remove(user!);
        DataSource.Users.Add(item);
    }
    /// <summary>
    ///  goes over the list of users and return the first user in the list on which the filter returns True.
    /// </summary>
    /// <param name="filter">a bool function</param>
    /// <returns>return the first engineer in the list on which the filter returns True</returns>
    public User? Read(Func<User, bool>? filter)//stage 2
    {
        if (filter == null)
        {
            return null;
        }
        else
            return DataSource.Users.FirstOrDefault(filter);
    }
    public void Clear() { DataSource.Users.Clear(); }
}
