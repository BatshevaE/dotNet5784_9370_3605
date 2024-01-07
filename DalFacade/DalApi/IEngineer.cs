namespace DalApi;
using DO;

public interface IEngineer
{
    int Create(Engineer item); //Creates new entity object of engineer in DAL
    Engineer? Read(int id); //Reads entity of engineer by its ID 
    List<Engineer> ReadAll(); //stage 1 only, Reads all entity objects of engineer
    void Update(Engineer item); //Updates entity of engineer
    void Delete(int id); //Deletes an object of engineer by its Id

}
