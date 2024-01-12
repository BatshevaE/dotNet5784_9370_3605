namespace DalApi;

public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates new entity of object in DAL
    /// </summary>
    /// <param name="item">Gets a object to add to the list</param>
    /// <returns>The id of the new object</returns>
    int Create(T item);
    /// <summary>
    /// Reads entity of by its ID 
    /// </summary>
    /// <param name="id">Gets an id of object to print</param>
    /// <returns>return the object to read</returns>
    T? Read(int id);
    /// <summary>
    /// stage 1 only, Reads all entity of objects
    /// </summary>
    /// <returns>return the list of objects</returns>
    //List<T> ReadAll();stage 1
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); // stage 2

    /// <summary>
    /// Updates entity of object
    /// </summary>
    /// <param name="item">Gets a object to update</param>
    void Update(T item);
    /// <summary>
    /// Deletes a object by its Id
    /// </summary>
    /// <param name="id">Gets an id of a object to delete</param>
    void Delete(int id);
    T? Read(Func<T, bool>? filter);//stage 2
}
