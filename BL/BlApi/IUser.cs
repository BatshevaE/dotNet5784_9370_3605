
using BO;

namespace BlApi;

public interface IUser
{
    /// <summary>
    /// creates a new user only if it's engineer in the system or a manager
    /// </summary>
    /// <param name="item">the user to add</param>
    /// <returns></returns>
    public int Create(BO.User item);
    /// <summary>
    /// returns user or null if it doesn't exist
    /// </summary>
    /// <param name="user1"></param>
    /// <returns></returns>
    public BO.User? Read(User user1);
    /// <summary>
    /// returns all users
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.User> ReadAll(Func<BO.User, bool>? filter = null);
    /// <summary>
    /// update a user
    /// </summary>
    /// <param name="item">the user to update</param>
    public void Update(BO.User item);
    /// <summary>
    /// deletes a user from the list
    /// </summary>
    /// <param name="id">id of user to delete</param>
    public void Delete(int id);
    /// <summary>
    /// clears all users from the list
    /// </summary>
    public void Clear();
}
