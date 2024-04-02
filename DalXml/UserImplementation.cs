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
        if (Read(item.Password) != null)
            throw new DalAlreadyExistException("An user type object with such an Password already exists");
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineer? eng = Engineers.FirstOrDefault(item1 => (item1.Name == item.Name)&&(item1.Id==item.Id));//stage 2
        if((eng==null)&&(item.Password!=111)&&(item.Password!=100))
        throw new DalDoesNotExistException("An user with such Name or Password can't be assigend to the system");
        Users.Add(item);
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);

        return item.Password;
    }
    /// <summary>
    /// The function deletes an existing engineer from the xml file of engineers
    /// </summary>
    /// <param name="id">ID number of an engineer to delete</param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public bool Delete(int password)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);

        User? ifUser = Users.Find(temp => temp.Password == password) ?? throw new DalDoesNotExistException($"User with Password: {password} does Not exist");
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
    public User? Read(int password)
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        User? user = Users.FirstOrDefault(item => (item.Password==password));//stage 2
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
        User? user = Users.Find(user1 => user1.Password == item.Password) ?? throw new DalDoesNotExistException($"User with Password: {item.Password} does Not exist");
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
    public void Clear()
    {
        List<User> Users = XMLTools.LoadListFromXMLSerializer<User>(s_users_xml);
        Users.Clear();
        XMLTools.SaveListToXMLSerializer(Users, s_users_xml);
    }
}
