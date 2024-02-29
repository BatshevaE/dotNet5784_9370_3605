
namespace BlApi;

public interface IUser
{
    public int Create(BO.User item);
    public BO.User? Read(int id);
    public IEnumerable<BO.User> ReadAll(Func<BO.User, bool>? filter = null);
    public void Update(BO.User item);
    public void Delete(int id);
    public void clear();
}
