namespace DalApi;
using DO;
public interface IDependency
{
    int Create(Dependency item); //Creates new entity of Dependency in DAL
    Dependency? Read(int id); //Reads entity of Dependency by its ID 
    List<Dependency> ReadAll(); //stage 1 only, Reads all entity of Dependencies
    void Update(Dependency item); //Updates entity of Dependency
    void Delete(int id); //Deletes an object of Dependency by its Id

}
