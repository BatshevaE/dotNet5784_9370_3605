namespace BlImplementation;
using BlApi;
using BO;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Engineer item)
    {
            if ((item.Id <= 0) || (item.Name == "") || (item.Email == null) || (item.Cost <= 0))
                throw new FormatException("wrong input");
        
        DO.Engineer doEngineer = new DO.Engineer
          (item.Id,item.Name,item.Email,(DO.EngineerLevel)item.Level,item.Cost);
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

    public void Delete(int id)
    {
       // DO.Engineer? doEngineer = _dal.Engineer.ReadAll().FirstOrDefault(temp => temp!.Id == id);
        //if (doEngineer == null)
        //    throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(temp => temp!.Engineerid == id);
        if (doTask != null)
            throw new BO.($"Cannot delete Engineer with ID={id}");
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", ex);
        }

    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        DO.Task? doTask = _dal.Task.ReadAll().FirstOrDefault(item=>item!.Id==id);
        Tuple<int, string>? EngTask;
        if (doTask != null)
        {
             EngTask = new Tuple<int, string>(doTask.Id, doTask.Name);
        }
        else
        {
             EngTask = null;
        }
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email= doEngineer.EmailAsress,
            Level= (BO.EngineerLevel)doEngineer.Complexity,
            Cost= doEngineer.CostForHour,
            Task = EngTask


        };

    }

    public IEnumerable<EngineerInTask> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
