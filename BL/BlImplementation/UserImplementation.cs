
namespace BlImplementation;
using BlApi;
using BO;
internal class UserImplementation:IUser
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.User item)
    {
        if ((item.Id <= 0) || (item.Name == ""))
            throw new BlWrongInput("wrong input");

        DO.User doUser = new DO.User
          (item.Id, item.Name, item.IsManager);
        try
        {
            int idUser = _dal.User.Create(doUser);
            return idUser;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"User doesn't exist or Engineer with ID={item.Id} already exists ", ex);
        }
    }
    public BO.User? Read(int id)
    {
        DO.User? doUser = _dal.User.Read(id);
        if (doUser == null)
            throw new BO.BlDoesNotExistException($"User with ID={id} does Not exist");
        return new BO.User()
        {
            Id = id,
            Name = doUser.Name,
            IsManager= doUser.IsManager
        };
    }
    public IEnumerable<BO.User> ReadAll(Func<BO.User, bool>? filter = null)
    {
        IEnumerable<DO.User?> UserList = _dal.User.ReadAll();
        IEnumerable<BO.User> BOUserList =
        from item in UserList
        orderby item.Id
        select new BO.User
        {
            Id = item.Id,
            Name = item.Name,
            IsManager = item.IsManager
        };
        if (filter != null) { return BOUserList.Where(filter); }
        else { return BOUserList; }
    }
    public void Update(BO.User item)
    {
        if ((item.Id <= 0) || (item.Name == ""))
            throw new BlWrongInput("wrong input");

        DO.User doUser = new DO.User
          (item.Id, item.Name!, item.IsManager);
        try
        {
            _dal.User.Update(doUser);

        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"User with ID={item.Id} already exists", ex);
        }
    }
    public void Delete(int id)
    {
        try
        {
            _dal.User.Delete(id);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"User with ID={id} does Not exist", ex);
        }
    }
    public void clear()
    {
        _dal.User.clear();
    }
}
