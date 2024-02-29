namespace Dal;
using DalApi;
using DO;
using System.Data.Common;

public class UserImplementation:IUser
{
    readonly string s_users_xml = "users";
    readonly string s_engineers_xml = "engineers";
    
    /// <summary>
    /// creats engineer in the xml file
    /// </summary>
    /// <param name="item">the engineer to add to the xml file</param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistException"></exception>
    public int Create(User item)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        if (Read(item.Id) != null)
            throw new DalAlreadyExistException("An user type object with such an ID already exists");
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? eng = Engineers.FirstOrDefault(item => item.Id == item.Id);//stage 2
        if((eng==null)&&(item.Id!=209859370)&&(item.Id!=32667605))
        throw new DalDoesNotExistException("An user with such Id can't be assigend to the system");
        Users.Add(item);
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);

        return item.Id;
    }
    /// <summary>
    /// The function deletes an existing engineer from the xml file of engineers
    /// </summary>
    /// <param name="id">ID number of an engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int id)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);

        User? ifUser= Users.Find(temp => temp.Id == id);
        if (ifUser == null)
        {
            throw new DalDoesNotExistException($"User with ID={id} does Not exist");
        }
        Users.Remove(ifUser);
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
        return true;
    }
    /// <summary>
    /// Returning a reference to a single object of Engineer with a certain ID from the xml file.
    /// </summary>
    /// <param name="id">ID number of an engineer.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    public User? Read(int id)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        User? user = Users.FirstOrDefault(item => item.Id == id);//stage 2
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
        return user;
    }
    /// <summary>
    /// Return a copy of the list of references to all objects of engineer from the xml file.
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of engineer.</returns>
    //public List<Engineer> ReadAll() stage 1
    public IEnumerable<User> ReadAll(Func<User, bool>? filter = null) //stage 2
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        if (filter != null)
        {
            XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
            return from item in Users
                   where filter(item)
                   select item;
        }
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
        return from item in Users
               select item;
    }
    /// <summary>
    /// Update of an existing object of enginer in the xml file.
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(User item)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        User? user = Users.Find(User => User.Id == item.Id);
        if (Users.FirstOrDefault(item) == null)
        {
            throw new DalDoesNotExistException($"User with ID={item.Id} does Not exist");
        }
        Users.Remove(user!);
        Users.Add(item);
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
    }
    /// <summary>
    ///  goes over the xml file of engineers and return the first engineer in the list on which the filter returns True.
    /// </summary>
    /// <param name="filter">a bool function</param>
    /// <returns>return the first engineer in the list on which the filter returns True</returns>
    public User? Read(Func<User, bool>? filter)//stage 2
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        if (filter == null)
        {
            XMLTools.SaveListToXMLSerializer(Users, s_users_xml);

            return null;
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
            return Users.FirstOrDefault(filter);
        }
    }
    /// <summary>
    /// deletes all the Engineers from the xml file
    /// </summary>
    public void clear()
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        Users.Clear();
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
    }
}
