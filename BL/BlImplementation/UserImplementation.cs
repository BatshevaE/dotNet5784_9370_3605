
namespace BlImplementation;
using BlApi;
using BO;
internal class UserImplementation:IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.User item)
    {
        if ((item.Password <= 0) || (item.Name == ""))
            throw new BlWrongInput("wrong input");

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
    public BO.User? Read(int password)
    {
        DO.User? doUser = _dal.User.Read(password);
        if (doUser == null)
            throw new BO.BlDoesNotExistException($"User with Password={password} does Not exist");
        return new BO.User()
        {
            Password = password,
            Name = doUser.Name,
            IsManager= doUser.IsManager,
            Id=doUser.Id
        };
    }
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
    public void clear()
    {
        _dal.User.clear();
    }
}
