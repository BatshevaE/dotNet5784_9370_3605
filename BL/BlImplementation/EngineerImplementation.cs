namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// the data source from the dal
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Adding a new engineer for the list
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <returns></returns>
    /// <exception cref="BlWrongInput">The method will return the running number of the newly created engineer in the list</exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public int Create(BO.Engineer item)
    {
        if ((item.Id <= 0) || (item.Name == "") ||  (item.Cost <= 0) )//check that the input is logically correct
            throw new BlWrongInput("wrong input");//throws exception

        DO.Engineer doEngineer = new (item.Id, item.Name, item.Email, (DO.EngineerLevel)item.Level, item.Cost);//creates a DO engineer with the item's details
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);//calls the lower layer to create the engineer to the data source
            return idEngineer;//returns the id of the new engineer
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={item.Id} already exists", ex);
        }

    }
    /// <summary>
    /// The function deletes an existing engineer from the list 
    /// </summary>
    /// <param name="id">ID number of an engineer</param>
    /// <exception cref="BO.BlCanNotDelete"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Engineerid == id);//search if the engineer doesn't assigned to any task
        if (doTask != null)//if there is an engineer that assign to a task
            throw new BO.BlCanNotDelete($"Cannot delete Engineer with ID={id}");
        try
        {
            _dal.Engineer.Delete(id);//calls the lower layer to delete the engineer from the data sourcr
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
        }

    }
    /// <summary>
    /// Returns a reference to a single object of Engineer with a certain ID.
    /// </summary>
    /// <param name="id">ID number of an engineer.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);//reads fron the lower layer the requested engineer
        return doEngineer == null
          ? throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")
            : new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.EmailAsress,
            Level = (BO.EngineerLevel)doEngineer.Complexity,
            Cost = doEngineer.CostForHour,
            Task = CalculateTaskInEngineer(id)
        };
    }
    /// <summary>
    /// Return a copy of the list of references to all objects of engineer.
    /// </summary>
    /// <returns>The method returns a new list that is a copy of the existing list of all objects of engineer.</returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<DO.Engineer?> EngineerList = _dal.Engineer.ReadAll();//a collection of all engineers frim the lower layer
            IEnumerable<BO.Engineer> BOEngineerList =
            from item in EngineerList
            orderby item.Id
            select new BO.Engineer
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.EmailAsress,
                Level = (BO.EngineerLevel)item.Complexity,
                Cost = item.CostForHour,
                Task = CalculateTaskInEngineer(item.Id)
            };//order the engineers by their id
        if (filter != null){ return BOEngineerList.Where(filter); }//returns the engineers that under the condition
        else { return BOEngineerList; }
    }
    /// <summary>
    /// Update of an existing object of enginer.
    /// </summary>
    /// <param name="item">A reference to an existing object of engineer.</param>
    /// <exception cref="BlWrongInput"></exception>
    /// <exception cref="BO.BlAlreadyExistException"></exception>
    public void Update(BO.Engineer item)
    {
        if ((item.Id <= 0) || (item.Name == "") || (item.Email == null) || (item.Cost <= 0)|| (item.Name == null))//check if the input is logically right
            throw new BlWrongInput("wrong input");

        DO.Engineer doEngineer = new (item.Id, item.Name!, item.Email, (DO.EngineerLevel)item.Level, item.Cost);
        try
        {
             _dal.Engineer.Update(doEngineer);//try to update the engineer in the data source via the lower layer
            
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={item.Id} already exists", ex);
        }
    }
    /// <summary>
    /// returns the task that assign to the engineer
    /// </summary>
    /// <param name="id">id of a task</param>
    /// <returns></returns>
   public List<Tuple<int,string>?>? CalculateTaskInEngineer(int id)
    {
        IEnumerable<DO.Task>? doTask = _dal.Task.ReadAll().Where(item => item!.Engineerid == id)!.ToList()!;//a list of all tasks that the engineer is assigned to
        List<Tuple<int, string>?>? EngTask;
        if ((doTask==null)||(doTask!.Count()==0))//if the list is empty
        {
            EngTask = null;//the engineer doesn't assign to any task

        }
        else
        {
            EngTask = doTask.Select(item => new Tuple<int, string>(item.Id, item.Name)).ToList()!;//a list of id+name of tasks that the enginner assigned to

        }
        return EngTask;
    }
    /// <summary>
    /// clear the data source
    /// </summary>
    public void Clear()
    {
       _dal.Engineer.Clear();//calls the lower layer to dekete all data about the engineers from the data source
    }
    /// <summary>
    /// return a collection of engineers in certain level
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public IEnumerable<BO.Engineer>? EngineersAtRequestedLevel(BO.EngineerLevel level)
    {
        IEnumerable<IGrouping <BO.EngineerLevel, BO.Engineer>> engineers = from item in ReadAll()
                                             group item by item.Level into gs
                                             select gs;//a collection of groups of engineers by their levels

        return engineers.FirstOrDefault(item=>item.Key == level);  //returns the requested group 
    }

}

