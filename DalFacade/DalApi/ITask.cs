
namespace DalApi;
using DO;
public interface InTask
{
    int Create(Task item); //Creates new entity of task in DAL
    Task? Read(int id); //Reads entity of task by its ID 
    List<Task> ReadAll(); //stage 1 only, Reads all entity of tasks
    void Update(Task item); //Updates entity of task
    void Delete(int id); //Deletes a task by its Id

}
