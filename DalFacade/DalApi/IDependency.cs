namespace DalApi;
using DO;
public interface IDependency
{
    /// <summary>
    ///Creates new entity of Dependency in DAL
    /// </summary>
    /// <param name="item">a dependency</param>
    /// <returns>return the id of the dependency</returns>
    int Create(Dependency item);
    /// <summary>
    ///Reads entity of Dependency by its ID 
    /// </summary>
    /// <param name="id">id os a dependency</param>
    /// <returns>the dpendency</returns>
    Dependency? Read(int id);
    /// <summary>
    ///stage 1 only, Reads all entity of Dependencies
    /// </summary>
    /// <returns>the list od dependencys</returns>
    List<Dependency> ReadAll();
    /// <summary>
    ///Updates entity of Dependency 
    /// </summary>
    /// <param name="item">a dependency</param>
    void Update(Dependency item);
    /// <summary>
    ///Deletes an object of Dependency by its Id
    /// </summary>
    /// <param name="id">an id of a dependency</param>
    void Delete(int id); 

}
