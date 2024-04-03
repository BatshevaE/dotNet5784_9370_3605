
namespace BlImplementation;
using BlApi;
using BO;
internal class UserImplementation:IUser
{
    /// <summary>
    /// the data source from the dal
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// create a new user only if there is such engineer in the system
    /// </summary>
    /// <param name="item">the user to create</param>
    /// <returns></returns>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public int Create(BO.User item)
    {
        if ((item.Password <= 0) || (item.Name == ""))//check the input
            throw new BlWrongInput("wrong input");
        if (!(ReadAll().Any())||ReadAll().Count()==0)
        {
            DO.User user = new(item.Password, item.Name, true, item.Id);
            return _dal.User.Create(user); 
           
        }
            
        DO.User doUser = new
          (item.Password, item.Name, item.IsManager,item.Id);
        try
        {
            int idUser = _dal.User.Create(doUser);
            return idUser;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"User doesn't exist or Engineer with Password={item.Password} already exists ", ex);
        }
    }
    /// <summary>
    /// read an user
    /// </summary>
    /// <param name="user1"></param>
    /// <returns></returns>
    public BO.User? Read(User user1)
    {
        DO.User? doUser = _dal.User.Read(user1.Password);//read a user from the lower layer
        if ((doUser == null)||(user1.Password!=doUser.Password)||(user1.Name!=doUser.Name)) 
            return null;//such user doesn't exist
        return new BO.User()
        {
            Password = doUser.Password,
            Name = doUser.Name,
            IsManager= doUser.IsManager,
            Id=doUser.Id
        };
    }
    /// <summary>
    /// a list of all users in the system
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.User> ReadAll(Func<BO.User, bool>? filter = null)
    {
        IEnumerable<DO.User?> UserList = _dal.User.ReadAll();
        IEnumerable<BO.User> BOUserList =
        from item in UserList
        orderby item.Password
        select new BO.User
        {
            Password = item.Password,
            Name = item.Name,
            IsManager = item.IsManager,
            Id = item.Id

        };
        if (filter != null) { return BOUserList.Where(filter); }
        else { return BOUserList; }
    }
    /// <summary>
    /// update an user
    /// </summary>
    /// <param name="item">the user to update</param>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public void Update(BO.User item)
    {
        if ((item.Password <= 0) || (item.Name == ""))
            throw new BlWrongInput("wrong input");

        DO.User doUser = new DO.User
          (item.Password, item.Name!, item.IsManager,item.Id);
        try
        {
            _dal.User.Update(doUser);

        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"User with Password={item.Password} already exists", ex);
        }
    }
    public void Delete(int password)
    {
        try
        {
            _dal.User.Delete(password);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"User with Password={password} does Not exist", ex);
        }
    }
    /// <summary>
    /// clear all data of user
    /// </summary>
    public void Clear()
    {
        _dal.User.Clear();
    }
}
