namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
        if ((item.Id <= 0) || (item.Name == "") || (item.Email == null) || (item.Cost <= 0))
            throw new BlWrongInput("wrong input");

        DO.Engineer doEngineer = new DO.Engineer
          (item.Id, item.Name, item.Email, (DO.EngineerLevel)item.Level, item.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
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
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Engineerid == id);
        if (doTask != null)//if there is an engineer that assign to a task
            throw new BO.BlCanNotDelete($"Cannot delete Engineer with ID={id}");
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
        }

    }
    /// <summary>
    /// Returning a reference to a single object of Engineer with a certain ID.
    /// </summary>
    /// <param name="id">ID number of an engineer.</param>
    /// <returns>If there is an object in the database with the received identification number, the method will return a reference to the existing engineer.
    ///Otherwise, the method will return null.</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        return new BO.Engineer()
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
        IEnumerable<DO.Engineer?> EngineerList = _dal.Engineer.ReadAll();
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
            };
        if (filter != null){ return BOEngineerList.Where(filter); }
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
        if ((item.Id <= 0) || (item.Name == "") || (item.Email == null) || (item.Cost <= 0))
            throw new BlWrongInput("wrong input");

        DO.Engineer doEngineer = new DO.Engineer
          (item.Id, item.Name, item.Email, (DO.EngineerLevel)item.Level, item.Cost);
        try
        {
             _dal.Engineer.Update(doEngineer);
            
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={item.Id} already exists", ex);
        }
    }
    /// <summary>
    /// return the task that assign to the engineer
    /// </summary>
    /// <param name="id">id of a task</param>
    /// <returns></returns>
   public Tuple<int,string>? CalculateTaskInEngineer(int id)
    { 
     DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(item => item!.Id == id);
    Tuple<int, string>? EngTask;
        if (doTask != null)
        {

            EngTask = new Tuple<int, string>(doTask.Id, doTask.Name);
        }
        else
        {
             EngTask = null;
        }
        return EngTask;
    }
    

    
}

