namespace DalApi;
using DO;

public interface IEngineer
{
    /// <summary>
    /// Creates new entity object of engineer in DAL
    /// </summary>
    /// <param name="item">an engineer</param>
    /// <returns>the id of the engineer</returns>
    int Create(Engineer item);
    /// <summary>
    /// Reads entity of engineer by its ID 
    /// </summary>
    /// <param name="id">an id of an engineer</param>
    /// <returns>return the engineer</returns>
    Engineer? Read(int id);
    /// <summary>
    /// Reads all entity objects of engineer
    /// </summary>
    /// <returns>return the list of enguneers</returns>
    List<Engineer> ReadAll();
    /// <summary>
    ///Updates entity of engineer 
    /// </summary>
    /// <param name="item">an engineer</param>
    void Update(Engineer item);
    /// <summary>
    ///Deletes an object of engineer by its Id
    /// </summary>
    /// <param name="id">an id of an engineer</param>
    void Delete(int id); 

}
